using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer
{
    public interface IReadRepository<T, Tkey> where T : class, IDomainObject<Tkey>
    {
        /// <summary>
        /// Возврашает запись по идентификатору
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Сущность или null, если не найдена</returns>
        T? ReadById(Tkey id);

        /// <summary>
        /// Возвращает все записи
        /// </summary>
        /// <returns>Список всех сущностей</returns>
        List<T> ReadAll();
    }
}
