
namespace Model
{
    public interface IRepository<T, Tkey> where T : class, IDomainObject<Tkey>
    {
        void Add(T item);

        bool Update(T item);

        bool Delete(Tkey id);

        T? ReadById(Tkey id);

        List<T> ReadAll();
    }
}
