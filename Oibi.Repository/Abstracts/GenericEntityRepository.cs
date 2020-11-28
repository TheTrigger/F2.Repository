using Microsoft.EntityFrameworkCore;
using Oibi.Repository.Interfaces;
using System;

namespace Oibi.Repository.Abstracts
{
    /// <summary>
    /// Generic repository to handle entities that have primary key
    /// </summary>
    public abstract class GenericEntityRepository<TEntity> : RepositoryEntityBase<TEntity, Guid> where TEntity : class, IEntity<Guid>, new()
    {
        protected GenericEntityRepository(DbContext repositoryContext) : base(repositoryContext)
        {
        }
    }
}
