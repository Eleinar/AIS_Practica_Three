namespace BusinessLogic
{
    /// <summary>
    /// Объединяет CRUD-операции и бизнес-функции.
    /// Является фасадом над <see cref="ICrudLogic"/> и <see cref="IBusinessLogic"/>.
    /// </summary>
    public interface ILogic : ICrudLogic, IBusinessLogic
    {
    }
}
