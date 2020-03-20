using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Oibi.Repository.Interfaces
{
    public interface IRepositoryEntity<T, PK> : IRepository<T> where T : class
    {
        T Retrieve(PK id);

        #region UPDATE

        T Update(PK id, [NotNull] T entity);

        T Update(PK id, [NotNull] object data);

        MAP Update<MAP>(PK id, [NotNull] T data);

        MAP Update<MAP>(PK id, [NotNull] object data);

        #endregion UPDATE

        #region DELETE

        T Delete(PK id);

        void DeleteRange(IEnumerable<PK> ids);

        #endregion DELETE
    }
}