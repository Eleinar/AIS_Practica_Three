using Model;

namespace DataAccessLayer
{
    /// <summary>
    /// Интерфейс записи сущностей (ISP: клиенты только для записи не зависят от чтения)
    /// </summary>
    /// <typeparam name="T">Тип сущности</typeparam>
    /// <typeparam name="Tkey">Тип первичного ключа</typeparam>
    public interface IWriteRepository<T, Tkey> where T : class, IDomainObject<Tkey>
    {
        /// <summary>
        /// Добавляет новую запись
        /// </summary>
        /// <param name="item">Сущность для добавления</param>
        void Add(T item);

        /// <summary>
        /// Обновляет существующую запись
        /// </summary>
        /// <param name="item">Сущность с новыми данными</param>
        /// <returns>true, если запись обновлена</returns>
        bool Update(T item);

        /// <summary>
        /// Удаляет запись по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор записи</param>
        /// <returns>true, если запись удалена</returns>
        bool Delete(Tkey id);
    }
}
