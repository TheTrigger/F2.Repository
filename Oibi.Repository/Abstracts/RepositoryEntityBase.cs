using Microsoft.EntityFrameworkCore;
using Oibi.Repository.Interfaces;
using System.Linq;

namespace Oibi.Repository.Abstracts
{
	public abstract partial class RepositoryEntityBase<TEntity, TPrimaryKey> : RepositoryBase<TEntity>
		where TEntity : class, IEntity<TPrimaryKey>, new()
		where TPrimaryKey : struct
	{
		protected RepositoryEntityBase(DbContext repositoryContext) : base(repositoryContext)
		{
		}

		/// <summary>
		/// Retrieve <see cref="TEntity"/> by <see cref="TPrimaryKey"/>
		/// </summary>
		/// <param name="id">Primary Key</param>
		public virtual TEntity Retrieve(TPrimaryKey id) => Set.Single(s => s.Id.Equals(id));

		public virtual TEntity Update(TPrimaryKey id, TEntity entity)
		{
			entity.Id = id;
			return Update(entity);
		}

		public virtual TEntity Delete(TPrimaryKey id)
		{
			var entity = new TEntity { Id = id };

			Set.Attach(entity);
			return Set.Remove(entity).Entity;
		}

		public virtual void DeleteRange(params TPrimaryKey[] ids)
		{
			var collection = ids.Select(id => new TEntity { Id = id });

			Set.AttachRange(collection);
			Set.RemoveRange(collection);
		}
	}
}