using System;
using System.Linq;
using System.Threading.Tasks;

namespace Oibi.Repository
{
    public interface IRepository<T, PK> : IQueryable<T>
    {
        IQueryable<T> FindAll();

        #region CREATE

        T Create(T entity);

        T Create(object data);

        M Create<M>(object data);

        /*
        Task<T> CreateAsync(T entity);

        Task<T> CreateAsync(object data);

        Task<M> CreateAsync<M>(object data);
        */

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