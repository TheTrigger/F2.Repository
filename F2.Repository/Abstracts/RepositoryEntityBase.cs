using F2.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace F2.Repository.Abstracts;

public abstract class RepositoryEntityBase<TEntity, TPrimaryKey> : RepositoryBase<TEntity>
	where TEntity : class, IEntity<TPrimaryKey>, new()
	where TPrimaryKey : struct, IEquatable<TPrimaryKey>
{
	protected RepositoryEntityBase(DbContext dbContext) : base(dbContext)
	{
	}

	/// <summary>
	/// Retrieve <see cref="TEntity"/> by <see cref="TPrimaryKey"/>
	/// </summary>
	/// <param name="id">Primary Key</param>
	public virtual async Task<TEntity?> RetrieveAsync(TPrimaryKey id, CancellationToken cancellationToken = default)
		=> await Set.SingleOrDefaultAsync(s => s.Id.Equals(id), cancellationToken);

	public virtual EntityEntry<TEntity> Update(TPrimaryKey id, TEntity entity)
	{
		if (!entity.Id.Equals(default(TPrimaryKey)) && !entity.Id.Equals(id))
		{
			throw new ArgumentException($"Entity Id '{entity.Id}' does not match the expected Id '{id}'.", nameof(entity));
		}

		entity.Id = id;
		return Set.Update(entity);
	}
}