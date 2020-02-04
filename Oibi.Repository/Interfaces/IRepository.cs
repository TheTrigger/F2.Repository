using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Oibi.Repository.Interfaces
{
    public interface IRepository<T> : IQueryable<T> where T : notnull
    {
        #region CREATE

        /// <summary>
        /// Create a new <see cref="T"/> entity
        /// </summary>
        T Create([DisallowNull] T entity);

        /// <summary>
        /// Create a new <see cref="T"/> entity from a compatible DTO
        /// </summary>
        T Create([DisallowNull] object data);

        /// <summary>
        /// Create a new <see cref="T"/> entity from a DTO then mapped to <see cref="MAP"/>
        /// </summary>
        MAP Create<MAP>([DisallowNull] object data);

        //ValueTask<T> CreateAsync(T entity);

        //ValueTask<T> CreateAsync(object data);

        //ValueTask<MAP> CreateAsync<MAP>(object data);

        #endregion CREATE

        #region UPDATE

        /// <summary>
        /// Update an entity
        /// </summary>
        T Update([DisallowNull] T entity);

        #endregion UPDATE

        #region DELETE

        /// <summary>
        /// Delete an entity
        /// </summary>
        T Delete([DisallowNull] T entity);

        void DeleteRange([DisallowNull] IEnumerable<T> entities);

        #endregion DELETE
    }
}