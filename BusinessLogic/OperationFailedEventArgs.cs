namespace BusinessLogic
{
    /// <summary>
    /// Аргументы события неудачной операции
    /// </summary>
    public class OperationFailedEventArgs : EventArgs
    {
        /// <summary>Текст причины для показа пользователю</summary>
        public string Message { get; }

        /// <summary>
        /// Создаёт аргументы события
        /// </summary>
        /// <param name="message">Текст причины</param>
        public OperationFailedEventArgs(string message)
        {
            Message = message;
        }
    }
}
