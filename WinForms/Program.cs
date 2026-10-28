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

            // Автоматическое внедрение IRepository в конструктор Logic
            Logic logic = ninjectKernel.Get<Logic>();

            Application.Run(new MainForm(logic));
        }
    }
}
