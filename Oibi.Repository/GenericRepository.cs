using Microsoft.EntityFrameworkCore;
using System;

namespace Oibi.Repository
{
    /// <summary>
    /// Dummy class to keep <see cref="RepositoryBase{T}"/> <see cref="abstract"/>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class GenericRepository<T> : RepositoryBase<T, Guid> where T : class, IEntity<Guid>, new()
    {
        protected GenericRepository(DbContext repositoryContext, AutoMapper.IMapper mapper) : base(repositoryContext, mapper)
        {
        }
    }
}