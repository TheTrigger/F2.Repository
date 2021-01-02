using System.Diagnostics.CodeAnalysis;

namespace Oibi.Repository.Interfaces
{
	/// <summary>
	/// <inheritdoc/>
	/// </summary>
	/// <typeparam name="TEntity"><inheritdoc/></typeparam>
	/// <typeparam name="TPrimaryKey">Primary key type</typeparam>
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