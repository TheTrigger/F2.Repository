using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Oibi.Repository
{
    public interface IRepository<T, PK> : IQueryable<T>
        where T : class
    {
        IQueryable<T> FindAll();

        #region CREATE

        T Create(T entity);

        T Create(object data);

        M Create<M>(object data);

        //ValueTask<EntityEntry<T>> CreateAsync(T entity);

        //ValueTask<EntityEntry<T>> CreateAsync(object data);

        //ValueTask<EntityEntry<M>> CreateAsync<M>(object data) where M : class;

        #endregion CREATE

        T Retrieve(PK id);

        #region UPDATE

        T Update(PK id, T entity);

        T Update(PK id, object data);

        M Update<M>(PK id, T data);

        M Update<M>(PK id, object data);

        T Update(T entity);

        #endregion UPDATE

        T Delete(T entity);

        T Delete(PK id);
    }
}