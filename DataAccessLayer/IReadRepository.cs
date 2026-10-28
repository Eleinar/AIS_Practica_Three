using Model;

namespace DataAccessLayer
{
    /// <summary>
    /// Интерфейс чтения сущностей (ISP: клиенты только для чтения не зависят от записи)
    /// </summary>
    /// <typeparam name="T">Тип сущности</typeparam>
    /// <typeparam name="Tkey">Тип первичного ключа</typeparam>
    public interface IReadRepository<T, Tkey> where T : class, IDomainObject<Tkey>
    {
        /// <summary>
        /// Возвращает запись по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор записи</param>
        /// <returns>Сущность или null, если не найдена</returns>
        T? ReadById(Tkey id);

        /// <summary>
        /// Возвращает все записи
        /// </summary>
        /// <returns>Список всех сущностей</returns>
        List<T> ReadAll();
    }
}
