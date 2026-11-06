using DataAccessLayer;
using Microsoft.EntityFrameworkCore;
using Ninject;
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
                .ToMethod(_ => new DbContextOptionsBuilder<AppDbContext>()
                    .UseNpgsql(ConnectionSettings.DBConnection())
                    .Options)
                .InSingletonScope();


            // Можно заменить на typeof(DapperRepository<,>), если нужна реализация на Dapper
            Bind(typeof(IRepository<,>))
                .To(typeof(EntityRepository<,>))
                .InSingletonScope();

            // IReadRepository отдаёт тот же singleton, что и IRepository
            Bind(typeof(IReadRepository<,>)).ToMethod(ctx =>
                ctx.Kernel.Get(typeof(IRepository<,>)
                    .MakeGenericType(ctx.Request.Service.GetGenericArguments())));

            Bind<ILicensePlateValidator>().To<LicensePlateValidator>().InSingletonScope();

            Bind<CrudLogic>().ToSelf();
            Bind<BusinessLogic>().ToSelf();
        }
    }
}
