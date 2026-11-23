using Presenter;

namespace WinForms
{
    internal static class Program
    {
        /// <summary>
        /// Точка входа: создаёт View и отдаёт его точке сборки, которая подключает Presenter и Model
        /// </summary>
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();

            var view = new MainForm();
            AppBootstrapper.Bind(view);

            Application.Run(view);
        }
    }
}
