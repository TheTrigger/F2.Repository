using System.Diagnostics.CodeAnalysis;

namespace Oibi.Repository.Interfaces
{
	public interface IRepositoryEntity<TEntity, TPrimaryKey> : IRepository<TEntity>
		where TPrimaryKey : struct
		where TEntity : class
	{
		TEntity Retrieve(TPrimaryKey id);

		TEntity Update(TPrimaryKey id, [DisallowNull] TEntity entity);

		TEntity Delete(TPrimaryKey id);

		void DeleteRange(params TPrimaryKey[] ids);
	}
}