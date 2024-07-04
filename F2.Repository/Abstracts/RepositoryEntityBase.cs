using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using F2.Repository.Interfaces;
using System;
using System.Linq;

namespace F2.Repository.Abstracts
{
	public abstract class RepositoryEntityBase<TEntity, TPrimaryKey> : RepositoryBase<TEntity>
		where TEntity : class, IEntity<TPrimaryKey>, new()
		where TPrimaryKey : IEquatable<TPrimaryKey>
	{
		protected RepositoryEntityBase(DbContext dbContext) : base(dbContext)
		{
		}

		/// <summary>
		/// Retrieve <see cref="TEntity"/> by <see cref="TPrimaryKey"/>
		/// </summary>
		/// <param name="id">Primary Key</param>
		public virtual TEntity Retrieve(TPrimaryKey id) => Set.Single(s => s.Id.Equals(id));

		public virtual EntityEntry<TEntity> Update(TPrimaryKey id, TEntity entity)
		{
			entity.Id = id;
			return Update(entity);
		}
	}
}