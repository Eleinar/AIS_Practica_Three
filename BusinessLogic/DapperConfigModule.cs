using DataAccessLayer;

namespace BusinessLogic
{
    /// <summary>
    /// Регистрация репозиториев на основе Dapper
    /// </summary>
    public class DapperConfigModule : SimpleConfigModule
    {

        public DapperConfigModule(string? connectionString = null) : base(connectionString) { }


        protected override void BindRepositories()
        {
            Bind(typeof(IRepository<,>)).To(typeof(DapperRepository<,>))
                .InSingletonScope()
                .WithConstructorArgument("connection", ConnectionString);
        }
    }
}