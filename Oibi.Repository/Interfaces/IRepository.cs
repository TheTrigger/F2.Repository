using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Oibi.Repository.Interfaces
{
	public interface IRepository<TEntity> : IAsyncEnumerable<TEntity>, IQueryable<TEntity> where TEntity : notnull
	{
		/// <summary>
		/// Create a new <typeparamref name="TEntity"/> <paramref name="entity"/>
		/// </summary>
		TEntity Create([DisallowNull] TEntity entity);

		/// <summary>
		/// Update an entity
		/// </summary>
		TEntity Update([DisallowNull] TEntity entity);

		/// <summary>
		/// Delete an entity
		/// </summary>
		TEntity Delete([DisallowNull] TEntity entity);

		/// <summary>
		/// Delete a collection of <typeparamref name="TEntity"/>
		/// </summary>
		void DeleteRange([DisallowNull] params TEntity[] entities);
	}
}