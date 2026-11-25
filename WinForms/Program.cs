using Presenter;

namespace WinForms
{
    internal static class Program
    {
        /// <summary>
        /// Точка входа WinForms-приложения. Форма создаётся здесь, а связывание View с Model
        /// выполняет точка сборки из Presenter (<see cref="AppComposer"/>)
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            MainForm view = new();
            IDisposable presenter;
            try
            {
                presenter = AppComposer.Compose(view);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Не удалось запустить приложение.\n\n" + (ex.InnerException?.Message ?? ex.Message),
                    "Автопарк", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            using (presenter)
            {
                Application.Run(view);
            }
        }
    }
}
