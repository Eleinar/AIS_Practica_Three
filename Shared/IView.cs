namespace Shared
{
    /// <summary>
    /// Presenter может потребовать от любого View
    /// </summary>
    public interface IView
    {
        /// <summary>
        /// Показывает сообщение об ошибке
        /// </summary>
        /// <param name="message">Текст сообщения</param>
        void ShowError(string message);

        /// <summary>
        /// Показывает информационное сообщение
        /// </summary>
        /// <param name="message">Текст сообщения</param>
        void ShowMessage(string message);
    }
}
