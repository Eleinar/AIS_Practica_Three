using DataAccessLayer;
using Microsoft.EntityFrameworkCore;
using Ninject.Modules;

namespace BusinessLogic
{
    /// <summary>
    /// Модуль Ninject: сопоставление абстракций с реализациями (DIP).
    /// Репозитории регистрируются как Singleton.
    /// </summary>
    public class SimpleConfigModule : NinjectModule
    {
        /// <summary>
        /// Регистрирует зависимости контейнера
        /// </summary>
        public override void Load()
        {
            // Параметры EF-контекста — один раз на приложение
            Bind<DbContextOptions<AppDbContext>>()
                .ToMethod(_ =>
                {
                    string cs = ConnectionSettings.DBConnection();
                    return new DbContextOptionsBuilder<AppDbContext>()
                        .UseNpgsql(cs)
                        .Options;
                })
                .InSingletonScope();

           
            // Можно заменить на typeof(DapperRepository<,>), если нужна реализация на Dapper
            Bind(typeof(IRepository<,>))
                .To(typeof(EntityRepository<,>))
                .InSingletonScope();

            // Бизнес-логика: зависимости подставятся автоматически через конструктор
            Bind<Logic>().ToSelf();

            Bind<ILicensePlateValidator>().To<LicensePlateValidator>().InSingletonScope();
        }
    }
}
