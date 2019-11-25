using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Oibi.Repository.Interfaces
{
    public interface IRepository<T> : IQueryable<T> where T : class
    {
        #region CREATE

        /// <summary>
        /// Create a new <see cref="T"/> entity
        /// </summary>
        T Create([NotNull] T entity);

        /// <summary>
        /// Create a new <see cref="T"/> entity from a compatible DTO
        /// </summary>
        T Create([NotNull] object data);

        /// <summary>
        /// Create a new <see cref="T"/> entity from a DTO then mapped to <see cref="MAP"/>
        /// </summary>
        MAP Create<MAP>([NotNull] object data);

        //ValueTask<T> CreateAsync(T entity);

        //ValueTask<T> CreateAsync(object data);

        //ValueTask<MAP> CreateAsync<MAP>(object data);

        #endregion CREATE

        #region UPDATE

        /// <summary>
        /// Update an entity
        /// </summary>
        T Update([NotNull] T entity);

        #endregion UPDATE

        #region DELETE

        /// <summary>
        /// Delete an entity
        /// </summary>
        T Delete([NotNull] T entity);

        #endregion DELETE
    }
}