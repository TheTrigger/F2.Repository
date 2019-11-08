using Microsoft.EntityFrameworkCore;
using Oibi.Repository.Interfaces;
using System;

namespace Oibi.Repository.Abstracts
{
    /// <summary>
    /// This <see cref="GenericRepository{T}"/> have contains "by-ID" methods (retrieve, delete). Your code-first classes have to implement <see cref="IEntity{PKT}"/>
    /// </summary>
    /// <typeparam name="T">Your database's table-type <see cref="object"/></typeparam>
    public abstract class GenericEntityRepository<T> : RepositoryEntityBase<T, Guid> where T : class, IEntity<Guid>, new()
    {
        protected GenericEntityRepository(DbContext repositoryContext, AutoMapper.IMapper mapper) : base(repositoryContext, mapper)
        {
        }
    }
}
