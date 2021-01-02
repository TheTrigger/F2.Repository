using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Oibi.Repository.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;

namespace Oibi.Repository.Abstracts
{
	public abstract partial class RepositoryBase<TEntity> : IRepository<TEntity>
		where TEntity : class, new()
	{
		protected readonly DbContext _context;

		protected DbSet<TEntity> Set { get; }

		protected RepositoryBase(DbContext dbContext)
		{
			_context = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
			Set = _context.Set<TEntity>();
		}

		/// <summary>
		/// Create new entity of type <see cref="TEntity"/>
		/// </summary>
		/// <param name="entity"></param>
		/// <returns></returns>
		public virtual EntityEntry<TEntity> Create(TEntity entity) => Set.Add(entity);

		/// <summary>
		/// <inheritdoc cref="Microsoft.EntityFrameworkCore.DbSet{TEntity}.Update(TEntity)"/>
		/// </summary>
		public virtual EntityEntry<TEntity> Update(TEntity entity)
		{
			return Set.Update(entity);
		}

		/// <summary>
		/// <inheritdoc cref="Microsoft.EntityFrameworkCore.DbSet{TEntity}.Remove(TEntity)"/>
		/// </summary>
		public virtual EntityEntry<TEntity> Remove(TEntity entity)
		{
			if (_context.Entry(entity).State == EntityState.Detached)
				Set.Attach(entity);
			return Set.Remove(entity);
		}

		public virtual void RemoveRange(params TEntity[] entities)
		{
			Set.AttachRange(entities);
			Set.RemoveRange(entities);
		}

		#region AS QUERYABLE

		/// <summary>
		/// <inheritdoc cref="Microsoft.EntityFrameworkCore.DbSet{TEntity}.AsQueryable"/>
		/// </summary>
		protected IQueryable<TEntity> Queryable => Set.AsQueryable();

		/// <summary>
		/// <inheritdoc/>
		/// </summary>
		public Type ElementType => typeof(TEntity);

		/// <summary>
		/// <inheritdoc/>
		/// </summary>
		public Expression Expression => Queryable.Expression;

		/// <summary>
		/// <inheritdoc/>
		/// </summary>
		public IQueryProvider Provider => Queryable.Provider;

		/// <summary>
		/// <inheritdoc/>
		/// </summary>
		/// <returns><inheritdoc/></returns>
		public IEnumerator GetEnumerator() => Queryable.GetEnumerator();

		/// <summary>
		/// <inheritdoc/>
		/// </summary>
		/// <returns><inheritdoc/></returns>
		IEnumerator<TEntity> IEnumerable<TEntity>.GetEnumerator() => Queryable.GetEnumerator();

		/// <summary>
		/// <inheritdoc/>
		/// </summary>
		/// <param name="cancellationToken"><inheritdoc/></param>
		/// <returns><inheritdoc/></returns>
		public IAsyncEnumerator<TEntity> GetAsyncEnumerator(CancellationToken cancellationToken = default)
			=> Set.AsAsyncEnumerable().GetAsyncEnumerator(cancellationToken);

		#endregion AS QUERYABLE
	}
}