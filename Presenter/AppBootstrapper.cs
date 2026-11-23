using BusinessLogic;
using Ninject;
using Shared;

namespace Presenter
{
    /// <summary>
    /// Точка сборки приложения: создаёт IoC-контейнер, получает Model и связывает её с View через Presenter
    /// </summary>
    public static class AppBootstrapper
    {
        private static IKernel? _kernel;

        /// <summary>
        /// Создаёт Presenter для переданного представления
        /// </summary>
        /// <param name="view">Главная форма (в виде интерфейса)</param>
        /// <returns>Созданный Presenter</returns>
        public static MainPresenter Bind(IMainView view)
        {
            _kernel ??= new StandardKernel(new SimpleConfigModule());
            IModel model = _kernel.Get<IModel>();
            return new MainPresenter(view, model);
        }
    }
}
