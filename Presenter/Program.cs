using BusinessLogic;
using Ninject;
using WinForms;

namespace Presenter
{
    /// <summary>
    /// Точка входа и точка сборки приложения (консольный проект Presenter):
    /// создаёт IoC-контейнер, получает Model, создаёт View и связывает их через Presenter
    /// </summary>
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            // Должно быть вызвано ДО создания первого окна (в том числе MainForm)
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            MainForm view;
            try
            {
                // IoC (Ninject): абстракции Model+BL → реализации
                IKernel kernel = new StandardKernel(new SimpleConfigModule());
                IModel model = kernel.Get<IModel>();

                // View создаётся здесь, Presenter получает её только как IMainView
                view = new MainForm();
                _ = new MainPresenter(view, model);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Не удалось запустить приложение.");
                Console.WriteLine(ex.InnerException?.Message ?? ex.Message);
                Console.WriteLine("\nНажмите Enter, чтобы выйти...");
                Console.ReadLine();
                return;
            }

            Console.WriteLine("Приложение запущено. Закройте окно, чтобы завершить работу.");

            Application.Run(view);
        }
    }
}
