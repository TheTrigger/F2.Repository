using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq;
using System.Threading.Tasks;

namespace Oibi.Repository.Interfaces
{
    public interface IRepository<T> : IQueryable<T> where T : class
    {
        #region CREATE

        /// <summary>
        /// Create a new <see cref="T"/> entity
        /// </summary>
        T Create(T entity);

        /// <summary>
        /// Create a new <see cref="T"/> entity from a compatible DTO
        /// </summary>
        T Create(object data);

        /// <summary>
        /// Create a new <see cref="T"/> entity from a DTO then mapped to <see cref="M"/>
        /// </summary>
        MAP Create<MAP>(object data);

        //ValueTask<T> CreateAsync(T entity);

        //ValueTask<T> CreateAsync(object data);

        //ValueTask<MAP> CreateAsync<MAP>(object data);

        #endregion CREATE

        #region UPDATE

        /// <summary>
        /// Update an entity
        /// </summary>
        T Update(T entity);

        #endregion UPDATE

        #region DELETE

        /// <summary>
        /// Delete an entity
        /// </summary>
        T Delete(T entity);

        #endregion DELETE
    }
}
