using BusinessLogic;
using Ninject;
using WinForms;

namespace Presenter
{
    /// <summary>
    /// Точка сборки приложения
    /// создаёт IoC-контейнер, получает Model, создаёт View и связывает их через Presenter
    /// </summary>
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            MainForm view;
            try
            {
                //Ninject
                IKernel kernel = new StandardKernel(new SimpleConfigModule());
                IModel model = kernel.Get<IModel>();

                
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
