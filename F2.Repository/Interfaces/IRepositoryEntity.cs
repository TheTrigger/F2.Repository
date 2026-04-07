using System.Diagnostics.CodeAnalysis;

namespace F2.Repository.Interfaces
{
	/// <summary>
	/// <inheritdoc/>
	/// </summary>
	/// <typeparam name="TEntity"><inheritdoc/></typeparam>
	/// <typeparam name="TPrimaryKey">Primary key type</typeparam>
	public interface IRepositoryEntity<TEntity, TPrimaryKey> : IRepository<TEntity>
		where TPrimaryKey : struct, IEquatable<TPrimaryKey>
		where TEntity : class, IEntity<TPrimaryKey>
	{
		Task<TEntity?> RetrieveAsync(TPrimaryKey id, CancellationToken cancellationToken = default);

		TEntity Update(TPrimaryKey id, [DisallowNull] TEntity entity);

		TEntity Delete(TPrimaryKey id);

		void DeleteRange(params TPrimaryKey[] ids);
	}
}