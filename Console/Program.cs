using System.Text;
using Presenter;

namespace ConsoleApp
{
    /// <summary>
    /// Консольное приложение автопарка. Создаёт View и отдаёт её точке сборки из Presenter;
    /// к бизнес-логике и БД напрямую не обращается
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Точка входа консольного приложения
        /// </summary>
        public static void Main(string[] args)
        {
            // Корректный вывод кириллицы в Windows-консоли
            Console.OutputEncoding = new UTF8Encoding(encoderShouldEmitUTF8Identifier: false);
            Console.InputEncoding = Encoding.UTF8;

            ConsoleMainView view = new();
            IDisposable presenter;
            try
            {
                presenter = AppComposer.Compose(view);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Не удалось запустить приложение.");
                Console.WriteLine(ex.InnerException?.Message ?? ex.Message);
                Console.WriteLine("\nНажмите Enter, чтобы выйти...");
                Console.ReadLine();
                return;
            }

            using (presenter)
            {
                view.Run();
            }
        }
    }
}
