using Microsoft.EntityFrameworkCore;

namespace Oibi.Repository.Abstracts
{
	/// <summary>
	/// Generic repository to handle entities without primary key
	/// </summary>
	/// <typeparam name="TEntity"><inheritdoc/></typeparam>
	public abstract class GenericRepository<TEntity> : RepositoryBase<TEntity> where TEntity : class, new()
	{
		protected GenericRepository(DbContext dbContext) : base(dbContext)
		{
		}
	}
}