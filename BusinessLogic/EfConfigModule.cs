using DataAccessLayer;
using Microsoft.EntityFrameworkCore;

namespace BusinessLogic
{
    /// <summary>
    /// Регистрация репозиториев на основе Entity Framework
    /// </summary>
    public class EfConfigModule : SimpleConfigModule
    {

        public EfConfigModule(string? connectionString = null) : base(connectionString) { }

       
        protected override void BindRepositories()
        {
            Bind<DbContextOptions<AppDbContext>>()
                .ToMethod(_ => new DbContextOptionsBuilder<AppDbContext>()
                    .UseNpgsql(ConnectionString)
                    .Options)
                .InSingletonScope();

            Bind(typeof(IRepository<,>)).To(typeof(EntityRepository<,>)).InSingletonScope();
        }
    }
}