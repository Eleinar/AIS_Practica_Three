using Microsoft.EntityFrameworkCore;
using Model;

namespace DataAccessLayer
{
    /// <summary>
    /// Контекст Entity Framework для работы с таблицами автомобилей и брендов
    /// </summary>
    public class AppDbContext : DbContext
    {
        /// <summary>
        /// Создаёт контекст с заданными параметрами
        /// </summary>
        /// <param name="options">Параметры подключения и настройки EF</param>
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        /// <summary>
        /// Набор автомобилей
        /// </summary>
        public DbSet<Car> Cars => Set<Car>();

        /// <summary>
        /// Набор брендов
        /// </summary>
        public DbSet<Brand> Brands => Set<Brand>();

        /// <summary>
        /// Настраивает связи и поведение модели
        /// </summary>
        /// <param name="modelBuilder">Построитель модели</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Car>()
                .HasOne<Brand>()
                .WithMany()
                .HasForeignKey(c => c.BrandID)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
