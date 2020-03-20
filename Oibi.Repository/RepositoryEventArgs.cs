using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;

namespace Oibi.Repository
{
    public class RepositoryEventArgs<TEntity> : EventArgs where TEntity : class
    {
        public RepositoryEventArgs(EntityEntry<TEntity> newEntityEntry)
        {
            Entry = newEntityEntry ?? throw new ArgumentNullException(nameof(newEntityEntry));
        }

        public RepositoryEventArgs(EntityEntry<TEntity> newEntityEntry, TEntity oldEntityEntry) : this(newEntityEntry)
        {
            Old = oldEntityEntry ?? throw new ArgumentNullException(nameof(oldEntityEntry));
        }

        public RepositoryEventArgs(TEntity oldEntityEntry)
        {
            Old = oldEntityEntry ?? throw new ArgumentNullException(nameof(oldEntityEntry));
        }

        public TEntity Old { get; }

        public TEntity New => Entry.Entity;

        public EntityEntry<TEntity> Entry { get; }
    }

    public class UpdatedEventArgs<TEntity> : EventArgs where TEntity : class
    {
        public UpdatedEventArgs(TEntity newT)
        {
            New = newT ?? throw new ArgumentNullException(nameof(newT));
        }

        public TEntity New { get; }
    }
}