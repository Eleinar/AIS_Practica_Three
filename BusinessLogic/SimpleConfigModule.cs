using DataAccessLayer;
using Ninject.Modules;

namespace BusinessLogic
{
        /// <summary>
        /// Общая часть регистрации зависимостей. Наследники регистрируют репозитории
        /// </summary>
        public abstract class SimpleConfigModule : NinjectModule
        {
            /// <summary>
            /// Строка подключения к базе данных
            /// </summary>
            protected string ConnectionString { get; }

            /// <param name="connectionString">Строка подключения или null для значения по умолчанию</param>
            protected SimpleConfigModule(string? connectionString = null)
            {
                ConnectionString = connectionString ?? ConnectionSettings.DBConnection();
            }

            /// <summary>
            /// Регистрирует общие зависимости и вызывает регистрацию репозиториев
            /// </summary>
            public override void Load()
            {
                Bind<ILicensePlateValidator>().To<LicensePlateValidator>().InSingletonScope();
                BindRepositories();
            }

            /// <summary>
            /// Сопоставляет IRepository с конкретной реализацией
            /// </summary>
            protected abstract void BindRepositories();
        }
}
