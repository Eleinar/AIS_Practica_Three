namespace WinForms
{
    internal static class Program
    {
        /// <summary>
        /// View не собирает приложение сама: точка сборки находится в проекте Presenter.
        /// Эта заглушка нужна только потому, что проект с формами остаётся WinExe (требование дизайнера форм).
        /// </summary>
        [STAThread]
        static void Main()
        {
            MessageBox.Show("Запускайте проект Presenter: он собирает приложение (View + Presenter + Model).",
                "Автопарк");
        }
    }
}
