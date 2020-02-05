using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Oibi.Repository.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Oibi.Repository.Abstracts
{
    public abstract partial class RepositoryBase<TEntity> : IRepository<TEntity> where TEntity : class, new()
    {
        /// <summary>
        /// TODO: to be disposed?
        /// </summary>
        private CancellationToken _cancellationToken = new CancellationToken();

        protected readonly DbContext _context;
        protected readonly IMapper _mapper;

        #region AS QUERYABLE

        protected IQueryable<TEntity> Queryable => Set.AsQueryable();

        public Type ElementType => typeof(TEntity);
        public Expression Expression => Queryable.Expression;
        public IQueryProvider Provider => Queryable.Provider;

        public IEnumerator GetEnumerator() => Queryable.GetEnumerator();

        IEnumerator<TEntity> IEnumerable<TEntity>.GetEnumerator() => Queryable.GetEnumerator();

        #endregion AS QUERYABLE

        private DbSet<TEntity> _set;
        /// <summary>
        /// TODO: make protected
        /// </summary>
        public DbSet<TEntity> Set => _set ??= _context.Set<TEntity>();

        protected RepositoryBase(DbContext repositoryContext, IMapper mapper)
        {
            _context = repositoryContext;
            _mapper = mapper;
        }

        #region CREATE

        /// <summary>
        /// Create new entity of type <see cref="TEntity"/>
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual TEntity Create(TEntity entity)
        {
            var ee = Set.Add(entity);

            OnCreated(new RepositoryEventArgs<TEntity>(ee));

            return ee.Entity;
        }

        /// <summary>
        /// Using <see cref="AutoMapper"/> to create <see cref="TEntity"/> resource
        /// </summary>
        /// <param name="data">An <see cref="object"/> mappable to <see cref="TEntity"/></param>
        public virtual TEntity Create(object data) => Set.Add(_mapper.Map<TEntity>(data)).Entity;

        /// <summary>
        /// Create a new entity from a dto and returns a dto using <see cref="AutoMapper"/>
        /// </summary>
        /// <typeparam name="TDestMap"></typeparam>
        /// <param name="data"></param>
        public virtual TDestMap Create<TDestMap>(object data) => _mapper.Map<TDestMap>(Create(data));

        #region ASYNC

        //public ValueTask<EntityEntry<T>> CreateAsync(T entity) => _context.Set<T>().AddAsync(entity);

        //public async ValueTask<MAP> CreateAsync<MAP>(T entity) => _mapper.Map<MAP>((await CreateAsync(entity).ConfigureAwait(false)).Entity);

        //public async ValueTask<MAP> CreateAsync<MAP>(object data) => _mapper.Map<MAP>((await CreateAsync<T>(data).ConfigureAwait(false)));

        #endregion ASYNC

        #endregion CREATE

        #region RETRIEVE

        // nothing here..

        #endregion RETRIEVE

        #region UPDATE

        public virtual TEntity Update(object data) => Set.Update(_mapper.Map<TEntity>(data)).Entity;

        public virtual TEntity Update(TEntity entity)
        {
            var ee = Set.Update(entity);

            OnUpdated(new RepositoryEventArgs<TEntity>(ee, entity));

            return ee.Entity;
        }

        public virtual TDestMap Update<TDestMap>(TEntity data) => _mapper.Map<TDestMap>(Update(data));

        #endregion UPDATE

        #region DELETE

        public virtual TEntity Delete(TEntity entity)
        {
            Set.Attach(entity);
            var ee = Set.Remove(entity);

            OnDeleted(new RepositoryEventArgs<TEntity>(ee, entity));

            return ee.Entity;
        }

        public virtual void DeleteRange(IEnumerable<TEntity> entities)
        {
            Set.AttachRange(entities);
            Set.RemoveRange(entities);
            entities.AsParallel().ForAll(e => OnDeleted(new RepositoryEventArgs<TEntity>(e)));
        }

        #endregion DELETE

        /// <summary>
        /// Cos'è sta roba. was high?
        /// </summary>
        /// <returns></returns>
        // public virtual IQueryable<TEntity> GetAll() => Set;

        // ok but useless. still high
        //public virtual IEnumerable<TDestMap> MapTo<TDestMap>(IEnumerable<TEntity> queryable) => _mapper.Map<IEnumerable<TDestMap>>(queryable);
        //public virtual IEnumerable<TDestMap> MapTo<TDestMap>() => _mapper.Map<IEnumerable<TDestMap>>(Set);

        // was high? - maybe its ok
        public IQueryable<TDestMap> ProjectTo<TDestMap>() => Set.ProjectTo<TDestMap>(_mapper.ConfigurationProvider);

        // toto extension with parameters and wrap? no you cant lol

        /// <summary>
        /// Fucking save data
        /// </summary>
        /// <returns></returns>
        protected Task<int> SaveChangesAsync() => _context.SaveChangesAsync(_cancellationToken);
    }
}