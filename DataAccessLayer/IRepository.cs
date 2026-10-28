using Model;

namespace DataAccessLayer
{

    /// <summary>
    /// Полный CRUD-репозиторий (объединяет чтение и запись).
    /// Является фасадом над IReadRepository и IWriteRepository.
    /// </summary>
    /// <typeparam name="T">Тип сущности</typeparam>
    /// <typeparam name="Tkey">Тип первичного ключа</typeparam>
    public interface IRepository<T, Tkey> : IReadRepository<T, Tkey>, IWriteRepository<T, Tkey>
        where T : class, IDomainObject<Tkey>
    {
    }
}
