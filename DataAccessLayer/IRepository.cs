using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer
{
    /// <summary>
    /// Фасад CRUD: объединяет чтение и изменение данных
    /// </summary>
    public interface IRepository<T, Tkey> : IReadRepository<T, Tkey>, IWriteRepository<T, Tkey>
        where T : class, IDomainObject<Tkey>
    {
    }
}
