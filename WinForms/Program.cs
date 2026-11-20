using BusinessLogic;
using Ninject;

namespace WinForms
{
    internal static class Program
    {
        /// <summary>
        /// Точка входа: создание IoC-контейнера и получение Logic через абстракции (DIP)
        /// </summary>
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();

            // IoC (Ninject): модуль из слоя BusinessLogic сопоставляет интерфейсы с реализациями
            IKernel ninjectKernel = new StandardKernel(new SimpleConfigModule());

            // Program зависит только от абстракции ILogic;
            // Logic, CrudLogic, BusinessLogic и репозитории создаёт контейнер
            ILogic logic = ninjectKernel.Get<ILogic>();
            Application.Run(new MainForm(logic));
        }
    }
}
