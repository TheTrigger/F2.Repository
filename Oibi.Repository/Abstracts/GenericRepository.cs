using Microsoft.EntityFrameworkCore;
using Oibi.Repository.Abstracts;

namespace Oibi.Repository
{
    /// <summary>
    /// This <see cref="GenericRepository{T}"/> doesn't have Retrieve and "by-ID" methods
    /// </summary>
    /// <typeparam name="TEntity">Your database's table-type <see cref="object"/></typeparam>
    public abstract class GenericRepository<TEntity> : RepositoryBase<TEntity> where TEntity : class, new()
    {
        protected GenericRepository(DbContext repositoryContext, AutoMapper.IMapper mapper) : base(repositoryContext, mapper)
        {
        }
    }
}