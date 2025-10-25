﻿using F2.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Collections;
using System.Linq.Expressions;

namespace F2.Repository.Abstracts;

public abstract class RepositoryBase<TEntity> : IRepository<TEntity>
	where TEntity : class, new()
{
	protected readonly DbContext _context;

	protected DbSet<TEntity> Set { get; }

	protected RepositoryBase(DbContext dbContext)
	{
		_context = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
		Set = _context.Set<TEntity>();
		_queryableSet = Set.AsQueryable();
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
    /// Marks multiple entities for deletion in the current context.
    /// Untracked entities are automatically attached to the context.
	/// </summary>
    /// <param name="entities">Array of entities to remove</param>
    /// <exception cref="ArgumentNullException">When entities array is null</exception>
    public virtual void RemoveRange(params TEntity[] entities)
	{
        ArgumentNullException.ThrowIfNull(entities);

        if (!entities.Any())
		{
            return;
		}

        // Filter and attach detached entities
        var detachedEntities = entities
            .Where(e => _context.Entry(e).State == EntityState.Detached)
            .ToArray();

        if (detachedEntities.Any())
        {
            Set.AttachRange(detachedEntities);
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
	protected readonly IQueryable<TEntity> _queryableSet;

	/// <summary>
	/// <inheritdoc/>
	/// </summary>
	public Type ElementType { get; } = typeof(TEntity);

	/// <summary>
	/// <inheritdoc/>
	/// </summary>
	public Expression Expression => _queryableSet.Expression;

	/// <summary>
	/// <inheritdoc/>
	/// </summary>
	public IQueryProvider Provider => _queryableSet.Provider;

	/// <summary>
	/// <inheritdoc/>
	/// </summary>
	/// <returns><inheritdoc/></returns>
	public IEnumerator GetEnumerator() => _queryableSet.GetEnumerator();

	/// <summary>
	/// <inheritdoc/>
	/// </summary>
	/// <returns><inheritdoc/></returns>
	IEnumerator<TEntity> IEnumerable<TEntity>.GetEnumerator() => _queryableSet.GetEnumerator();

	/// <summary>
	/// <inheritdoc/>
	/// </summary>
	/// <param name="cancellationToken"><inheritdoc/></param>
	/// <returns><inheritdoc/></returns>
	public IAsyncEnumerator<TEntity> GetAsyncEnumerator(CancellationToken cancellationToken = default)
		=> Set.AsAsyncEnumerable().GetAsyncEnumerator(cancellationToken);

	#endregion AS QUERYABLE
}