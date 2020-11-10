using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Oibi.Repository.Interfaces
{
    public interface IRepository<TEntity> : IAsyncEnumerable<TEntity>, IQueryable<TEntity> where TEntity : notnull
    {
        #region CREATE

        /// <summary>
        /// Create a new <see cref="TEntity"/> entity
        /// </summary>
        TEntity Create([DisallowNull] TEntity entity);

        /// <summary>
        /// Create a new <see cref="TEntity"/> entity from a compatible DTO
        /// </summary>
        TEntity Create([DisallowNull] object data);

        /// <summary>
        /// Create a new <see cref="TEntity"/> entity from a DTO then mapped to <see cref="MAP"/>
        /// </summary>
        MAP Create<MAP>([DisallowNull] object data);

        #endregion CREATE

        #region UPDATE

        /// <summary>
        /// Update an entity
        /// </summary>
        TEntity Update([DisallowNull] TEntity entity);

        #endregion UPDATE

        #region DELETE

        /// <summary>
        /// Delete an entity
        /// </summary>
        TEntity Delete([DisallowNull] TEntity entity);

        void DeleteRange([DisallowNull] IEnumerable<TEntity> entities);

        #endregion DELETE
    }
}