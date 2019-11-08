using Microsoft.EntityFrameworkCore;
using Oibi.Repository.Abstracts;
using Oibi.Repository.Interfaces;
using System;

namespace Oibi.Repository
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

    /// <summary>
    /// This <see cref="GenericRepository{T}"/> doesn't have Retrieve and "by-ID" methods
    /// </summary>
    /// <typeparam name="T">Your database's table-type <see cref="object"/></typeparam>
    public class GenericRepository<T> : RepositoryBase<T> where T : class, new()
    {
        protected GenericRepository(DbContext repositoryContext, AutoMapper.IMapper mapper) : base(repositoryContext, mapper)
        {
        }
    }
}