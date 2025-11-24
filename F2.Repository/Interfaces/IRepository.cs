using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace F2.Repository.Interfaces
{
	/// <summary>
	/// Exposes basic CRUD operations.
	/// <inheritdoc cref="IAsyncEnumerable{T}"/>
	/// <inheritdoc cref="IQueryable{T}"/>
	/// </summary>
	/// <typeparam name="TEntity"><inheritdoc/></typeparam>
	public interface IRepository<TEntity> : IAsyncEnumerable<TEntity>, IQueryable<TEntity>, IRepository where TEntity : class
	{
		/// <summary>
		/// Delete an <paramref name="entity"/>
		/// </summary>
		EntityEntry<TEntity> Remove([DisallowNull] TEntity entity);

		/// <summary>
		/// Delete a collection of <typeparamref name="TEntity"/>
		/// </summary>
		void RemoveRange([DisallowNull] params TEntity[] entities);
	}

	public interface IRepository
	{

	}
}