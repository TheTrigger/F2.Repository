using System;

namespace Oibi.Repository.Abstracts
{
    public partial class RepositoryBase<TEntity>
    {
        #region EVENTS

        /// <summary>
        /// Raise after calling <see cref="RepositoryBase{TEntity}.Create(TEntity)"/>
        /// </summary>
        public static event EventHandler<RepositoryEventArgs<TEntity>> Created;

        /// <summary>
        /// Raise after calling <see cref="RepositoryBase{TEntity}.Update(TEntity)"/>
        /// </summary>
        public static event EventHandler<RepositoryEventArgs<TEntity>> Updated;

        /// <summary>
        /// Raise after calling <see cref="RepositoryBase{TEntity}.Delete(TEntity)"/>
        /// </summary>
        public static event EventHandler<RepositoryEventArgs<TEntity>> Deleted;

        protected virtual void OnCreated(RepositoryEventArgs<TEntity> e) => Created?.Invoke(this, e);

        protected virtual void OnUpdated(RepositoryEventArgs<TEntity> e) => Updated?.Invoke(this, e);

        protected virtual void OnDeleted(RepositoryEventArgs<TEntity> e) => Deleted?.Invoke(this, e);

        #endregion EVENTS
    }
}