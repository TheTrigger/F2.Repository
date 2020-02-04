using System;

namespace Oibi.Repository.Abstracts
{
    public abstract partial class RepositoryEntityBase<TEntity, TKey>
    {
        public static event EventHandler<UpdatedEventArgs<TEntity>> Retrieved;

        protected virtual void OnRetrieved(UpdatedEventArgs<TEntity> e) => Retrieved?.Invoke(this, e);
    }
}