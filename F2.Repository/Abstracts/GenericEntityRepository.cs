using F2.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace F2.Repository.Abstracts;

/// <summary>
/// Generic repository to handle entities that have primary key
/// </summary>
public abstract class GenericEntityRepository<TEntity> : RepositoryEntityBase<TEntity, Guid>
	where TEntity : class, IEntity<Guid>, new()
{
	protected GenericEntityRepository(DbContext dbContext) : base(dbContext)
	{
	}
}