using Model;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer
{
    /// <summary>
    /// Реализация репозитория на основе Entity Framework Core
    /// </summary>
    /// <typeparam name="T">Тип сущности</typeparam>
    /// <typeparam name="TKey">Тип первичного ключа</typeparam>
    public class EntityRepository<T, TKey> : IRepository<T, TKey> where T : class, IDomainObject<TKey>
    {
        private readonly DbContextOptions<AppDbContext> _options;

        ///<inheritdoc/>
        public EntityRepository(DbContextOptions<AppDbContext> options)
        {
            _options = options;
        }

        ///<inheritdoc/>
        private AppDbContext CreateContext()
        {
            return new AppDbContext(_options);
        }

        ///<inheritdoc/>
        public virtual void Add(T entity)
        {
            using AppDbContext context = CreateContext();
            context.Set<T>().Add(entity);
            context.SaveChanges();
        }

        ///<inheritdoc/>
        public virtual bool Update(T entity)
        {
            using AppDbContext context = CreateContext();
            context.Set<T>().Update(entity);

            try
            {
                context.SaveChanges();
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                return false;
            }
        }

        ///<inheritdoc/>
        public virtual bool Delete(TKey id)
        {
            using AppDbContext context = CreateContext();
            T? entity = context.Set<T>().Find(new object?[] { id });

            if (entity == null)
            {
                return false;
            }

            context.Set<T>().Remove(entity);
            context.SaveChanges();
            return true;
        }

        ///<inheritdoc/>
        public virtual T? ReadById(TKey id)
        {
            using AppDbContext context = CreateContext();
            return context.Set<T>().Find(new object?[] { id });
        }

        ///<inheritdoc/>
        public virtual List<T> ReadAll()
        {
            using AppDbContext context = CreateContext();
            return context.Set<T>().AsNoTracking().ToList();
        }
    }
}
