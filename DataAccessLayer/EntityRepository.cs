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

        /// <summary>
        /// Создаёт репозиторий с заданными параметрами контекста
        /// </summary>
        /// <param name="options">Параметры DbContext</param>
        public EntityRepository(DbContextOptions<AppDbContext> options)
        {
            _options = options;
        }

        /// <summary>
        /// Создаёт новый экземпляр контекста базы данных
        /// </summary>
        /// <returns>Контекст EF</returns>
        private AppDbContext CreateContext()
        {
            return new AppDbContext(_options);
        }

        /// <summary>
        /// Добавляет сущность в базу данных
        /// </summary>
        /// <param name="entity">Добавляемая сущность</param>
        public virtual void Add(T entity)
        {
            using AppDbContext context = CreateContext();
            context.Set<T>().Add(entity);
            context.SaveChanges();
        }

        /// <summary>
        /// Обновляет сущность в базе данных
        /// </summary>
        /// <param name="entity">Сущность с новыми данными</param>
        /// <returns>true, если сохранение прошло успешно</returns>
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

        /// <summary>
        /// Удаляет сущность по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор сущности</param>
        /// <returns>true, если сущность найдена и удалена</returns>
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

        /// <summary>
        /// Возвращает сущность по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор сущности</param>
        /// <returns>Сущность или null</returns>
        public virtual T? ReadById(TKey id)
        {
            using AppDbContext context = CreateContext();
            return context.Set<T>().Find(new object?[] { id });
        }

        /// <summary>
        /// Возвращает все сущности без отслеживания изменений
        /// </summary>
        /// <returns>Список всех сущностей</returns>
        public virtual List<T> ReadAll()
        {
            using AppDbContext context = CreateContext();
            return context.Set<T>().AsNoTracking().ToList();
        }
    }
}
