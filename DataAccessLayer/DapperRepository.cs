using Model;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
using Dapper;
using Npgsql;

namespace DataAccessLayer
{
    public  class DapperRepository<T, Tkey> : IRepository<T, Tkey> where T : class, IDomainObject<Tkey>
    {
        private readonly string _connectionString;
        private readonly PropertyInfo _keyProperty;
        private readonly bool _keyGenerated;
        private readonly string _selectAll;
        private readonly string _selectById;
        private readonly string _insert;
        private readonly string _update;
        private readonly string _delete;

        public DapperRepository(string connection)
        {
            _connectionString = connection ?? throw new ArgumentNullException(nameof(connection));

            Type type = typeof(T);

            string table = type.GetCustomAttribute<TableAttribute>()?.Name
                ?? throw new InvalidOperationException($"Тип {type.Name} не помечен атрибутом [Table].");

            PropertyInfo[] props = type.GetProperties(BindingFlags.Public | BindingFlags.Instance).
                Where(p => p.CanRead && p.CanWrite && p.GetCustomAttribute<NotMappedAttribute>() == null).ToArray();

            _keyProperty = props.SingleOrDefault(p => p.GetCustomAttribute<KeyAttribute>() != null)
                ?? throw new InvalidOperationException($"Тип {type.Name} не имеет свойства с атрибутом [Key].");

               _keyGenerated = _keyProperty.GetCustomAttribute<DatabaseGeneratedAttribute>()?.DatabaseGeneratedOption == DatabaseGeneratedOption.Identity;

            PropertyInfo[] dataProps = props.Where(p => p != _keyProperty).ToArray();
            PropertyInfo[] insertProps = _keyGenerated ? dataProps : props;

            string keyColumn = Column(_keyProperty);
            string selectColumns = string.Join(", ", props.Select(SelectItem));
            string insertColumnList = string.Join(", ", insertProps.Select(Column));
            string insertValueList = string.Join(", ", insertProps.Select(p => "@" + p.Name));
            string updateSet = string.Join(", ", dataProps.Select(p => Column(p) + " = @" + p.Name));

            _selectAll = $"SELECT {selectColumns} FROM {table}";
            _selectById = $"SELECT {selectColumns} FROM {table} WHERE {keyColumn} = @Id";
            _insert = $"INSERT INTO {table} ({insertColumnList}) VALUES ({insertValueList})";
            if(_keyGenerated)
            {
                _insert += $" RETURNING {keyColumn}";
            }
            _update = $"UPDATE {table} SET {updateSet} WHERE {keyColumn} = @{_keyProperty.Name}";
            _delete = $"DELETE FROM {table} WHERE {keyColumn} = @Id";

        }

        private static string ColumnName(PropertyInfo property)
        {
            return property.GetCustomAttribute<ColumnAttribute>()?.Name ?? property.Name;
        }

        private static string Column(PropertyInfo property)
        {
            return "\"" + ColumnName(property) + "\"";
        }

        private static string SelectItem(PropertyInfo property)
        {
            return Column(property) + " AS \"" + property.Name + "\"";
        }

        private static DynamicParameters IdParameter(Tkey id)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@Id", id);
            return parameters;
        }

        public void Add(T item)
        {
            using NpgsqlConnection connection = new NpgsqlConnection(_connectionString);
            if (_keyGenerated)
            {
                Tkey newKey = connection.ExecuteScalar<Tkey>(_insert, item);
                _keyProperty.SetValue(item, newKey);
            }
            else
            {
                connection.Execute(_insert, item);
            }
        }

        public bool Update(T item)
        {
            using NpgsqlConnection connection = new(_connectionString);
            return connection.Execute(_update, item) > 0;
        }

        public bool Delete(Tkey id)
        {
            using NpgsqlConnection connection = new(_connectionString);
            return connection.Execute(_delete, IdParameter(id)) > 0;
        }

        public T? ReadById(Tkey id)
        {
            using NpgsqlConnection connection = new(_connectionString);
            return connection.QuerySingleOrDefault<T>(_selectById, IdParameter(id));
        }

        public List<T> ReadAll()
        {
            using NpgsqlConnection connection = new(_connectionString);
            return connection.Query<T>(_selectAll).ToList();
        }

    }
}
