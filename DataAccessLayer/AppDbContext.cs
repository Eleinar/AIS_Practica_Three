using Microsoft.EntityFrameworkCore;
using Model;
namespace DataAccessLayer
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}

        public DbSet<Car> Cars => Set<Car>();
        public DbSet<Brand> Brands => Set<Brand>();

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
