using System.Linq;

namespace Oibi.Repository.Interfaces
{
    public interface IRepositoryEntity<T, PK> : IRepository<T> where T : class
    {
        T Retrieve(PK id);

        #region UPDATE

        T Update(PK id, T entity);

        T Update(PK id, object data);

        MAP Update<MAP>(PK id, T data);

        MAP Update<MAP>(PK id, object data);

        #endregion UPDATE

        #region DELETE

        T Delete(PK id);

        #endregion DELETE
    }
}