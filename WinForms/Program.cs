using BusinessLogic;
using Ninject;

namespace WinForms
{
    internal static class Program
    {
        /// <summary>
        /// Точка входа: создание IoC-контейнера и получение Logic с внедрёнными зависимостями
        /// </summary>
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();

            // IoC (Ninject): модуль из слоя BusinessLogic регистрирует IRepository 
            IKernel ninjectKernel = new StandardKernel(new SimpleConfigModule());

            // Автоматическое внедрение IRepository в конструктор CrudLogic
            CrudLogic crud = ninjectKernel.Get<CrudLogic>();
            BusinessLogic.BusinessLogic business = ninjectKernel.Get<BusinessLogic.BusinessLogic>();
            Application.Run(new MainForm(crud, business));
        }
    }
}
