using Model;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer
{
    public class EntityRepository<T, TKey> : IRepository<T, TKey> where T : class, IDomainObject<TKey>
    {
        private readonly DbContextOptions<AppDbContext> _options;

        public EntityRepository(DbContextOptions<AppDbContext> options)
        {
            _options = options;
        }

        private AppDbContext CreateContext()
        {
            return new AppDbContext(_options);
        }
         
        public void Add(T entity)
        { 
            using AppDbContext context = CreateContext();
            context.Set<T>().Add(entity);
            context.SaveChanges();
        }

        public bool Update(T entity)
        {
            using AppDbContext context = CreateContext();
            context.Set<T>().Update(entity);

            try
            {
                context.SaveChanges();
                return true;
            }
            catch(DbUpdateConcurrencyException)
            {
                return false;
            }
        }

        public bool Delete(TKey id)
        {
            using AppDbContext context = CreateContext();
            T? entity = context.Set<T>().Find(new object?[] {id});

            if (entity == null)
            {
                return false;
            }

            context.Set<T>().Remove(entity);
            context.SaveChanges();
            return true;
        }

        public T? ReadById(TKey id)
        {
            using AppDbContext context = CreateContext();
            return context.Set<T>().Find(new object?[] { id });
        }

       public List<T> ReadAll()
        {
            using AppDbContext context = CreateContext();
            return context.Set<T>().AsNoTracking().ToList();
        }

    }
}
