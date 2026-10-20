namespace Model
{
    /// <summary>
    /// Маркерный интерфейс доменного объекта с идентификатором
    /// </summary>
    /// <typeparam name="Tkey">Тип первичного ключа</typeparam>
    public interface IDomainObject<Tkey>
    {
        /// <summary>
        /// Уникальный идентификатор объекта
        /// </summary>
        Tkey ID { get; }
    }
}
