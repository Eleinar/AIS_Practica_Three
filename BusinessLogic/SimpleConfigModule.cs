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
            // Параметры EF-контекста 
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

            // Logic: CRUD + бизнес-функции
            Bind<ICrudLogic>().To<CrudLogic>();
            Bind<IBusinessLogic>().To<BusinessLogic>();
            Bind<ILogic>().To<Logic>();

            // Model для Presenter: единственная точка входа в слой Model+BL
            Bind<IModel>().To<FleetModel>().InSingletonScope();
        }
    }
}
