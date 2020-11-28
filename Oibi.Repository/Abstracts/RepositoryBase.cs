using Microsoft.EntityFrameworkCore;
using Oibi.Repository.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;

namespace Oibi.Repository.Abstracts
{
	public abstract partial class RepositoryBase<TEntity> : IRepository<TEntity> where TEntity : class, new()
	{
		protected readonly DbContext _context;

		private DbSet<TEntity> _set;

		/// <summary>
		/// TODO: make protected
		/// </summary>
		public DbSet<TEntity> Set => _set ??= _context.Set<TEntity>();

		protected RepositoryBase(DbContext repositoryContext)
		{
			_context = repositoryContext;
		}

		/// <summary>
		/// Create new entity of type <see cref="TEntity"/>
		/// </summary>
		/// <param name="entity"></param>
		/// <returns></returns>
		public virtual TEntity Create(TEntity entity)
		{
			var ee = Set.Add(entity);
			return ee.Entity;
		}

		public virtual TEntity Update(TEntity entity)
		{
			var ee = Set.Update(entity);
			return ee.Entity;
		}

		public virtual TEntity Delete(TEntity entity)
		{
			Set.Attach(entity);
			var ee = Set.Remove(entity);
			return ee.Entity;
		}

		public virtual void DeleteRange(params TEntity[] entities)
		{
			Set.AttachRange(entities);
			Set.RemoveRange(entities);
		}

		#region AS QUERYABLE

		/// <summary>
		/// <inheritdoc/>
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