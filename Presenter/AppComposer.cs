using BusinessLogic;
using Ninject;
using Shared;

namespace Presenter
{
    /// <summary>
    /// Точка сборки приложения
    /// Создаёт контейнер, получает Model и связывает её с переданной View через Presenter.
    /// </summary>
    public static class AppComposer
    {
        private static IKernel? _kernel;

        /// <summary>
        /// Связывает View и Model
        /// </summary>
        /// <param name="view">Представление, которое нужно «обслуживать»</param>
        /// <returns>Presenter; его нужно освободить (Dispose) при закрытии View</returns>
        public static IDisposable Compose(IMainView view)
        {
            ArgumentNullException.ThrowIfNull(view);

            _kernel ??= new StandardKernel(new SimpleConfigModule());
            IModel model = _kernel.Get<IModel>();

            return new MainPresenter(view, model);
        }
    }
}
