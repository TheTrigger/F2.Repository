using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Oibi.Repository.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Oibi.Repository.Abstracts
{
    public abstract partial class RepositoryEntityBase<TEntity, TKey> : RepositoryBase<TEntity>
        where TEntity : class, IEntity<TKey>, new()
        where TKey : struct
    {
        protected RepositoryEntityBase(DbContext repositoryContext, IMapper mapper) : base(repositoryContext, mapper)
        {
        }

        #region RETRIEVE

        /// <summary>
        /// Retrieve <see cref="TEntity"/> by <see cref="TKey"/>
        /// </summary>
        /// <param name="id">Primary Key</param>
        public virtual TEntity Retrieve(TKey id) => Set.Single(s => s.Id.Equals(id));

        /// <summary>
        /// Retrieve <see cref="TEntity"/> mapped to <see cref="MAP"/>
        /// </summary>
        public MAP Retrieve<MAP>(TKey id) => _mapper.Map<MAP>(Set.Single(s => s.Id.Equals(id)));
        //public virtual MAP Retrieve<MAP>(TKey id) where MAP : IEntity<TKey>
            //=> Set.ProjectTo<MAP>(_mapper.ConfigurationProvider).Single(s => s.Id.Equals(id));

        #endregion RETRIEVE

        #region UPDATE

        public virtual TEntity Update(TKey id, TEntity entity)
        {
            entity.Id = id;
            return Update(entity);
        }

        public virtual TEntity Update(TKey id, object data) => Update(id, _mapper.Map<TEntity>(data));

        public virtual MAP Update<MAP>(TKey id, TEntity data) => _mapper.Map<MAP>(Update(id, data));

        /// <summary>
        /// Update from Dto entity by id and map to <see cref="MAP"/>
        /// </summary>
        /// <typeparam name="MAP">Destination type</typeparam>
        /// <param name="id">Primary Key</param>
        /// <param name="data">Dto data</param>
        public virtual MAP Update<MAP>(TKey id, object data) => _mapper.Map<MAP>(Update(id, data));

        #endregion UPDATE

        #region DELETE

        public virtual TEntity Delete(TKey id)
        {
            var entity = new TEntity { Id = id };

            Set.Attach(entity);
            return Set.Remove(entity).Entity;
        }

        public virtual void DeleteRange(IEnumerable<TKey> ids)
        {
            var collection = ids.Select(id => new TEntity { Id = id });

            Set.AttachRange(collection);
            Set.RemoveRange(collection);
        }

        #endregion DELETE
    }
}