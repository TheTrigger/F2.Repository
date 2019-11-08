using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Oibi.Repository
{
    public abstract class RepositoryBase<T, PK> : IRepository<T, PK>
        where T : class, IEntity<PK>, new()
        where PK : struct, IEquatable<PK>
    {
        protected DbContext _context;

        protected IMapper _mapper;

        #region AS QUERYABLE

        public Type ElementType => typeof(T);
        public Expression Expression => _context.Set<T>().AsQueryable().Expression;
        public IQueryProvider Provider => _context.Set<T>().AsQueryable().Provider;

        IEnumerator IEnumerable.GetEnumerator() => _context.Set<T>().AsQueryable().GetEnumerator();

        public IEnumerator GetEnumerator() => _context.Set<T>().AsQueryable().GetEnumerator();

        IEnumerator<T> IEnumerable<T>.GetEnumerator() => _context.Set<T>().AsQueryable().GetEnumerator();

        #endregion AS QUERYABLE

        protected RepositoryBase(DbContext repositoryContext, IMapper mapper)
        {
            _context = repositoryContext;
            _mapper = mapper;
        }

        public IQueryable<T> FindAll()
        {
            return _context.Set<T>().AsNoTracking();
        }

        #region CREATE

        public T Create(T entity) => _context.Set<T>().Add(entity).Entity;

        /// <summary>
        /// Using <see cref="AutoMapper"/> to create <see cref="T"/> resource
        /// </summary>
        /// <param name="data">An <see cref="object"/> mappable to <see cref="T"/></param>
        public T Create(object data) => _context.Set<T>().Add(_mapper.Map<T>(data)).Entity;

        public M Create<M>(object data) => _mapper.Map<M>(Create(data));

        #region ASYNC

        //public ValueTask<EntityEntry<T>> CreateAsync(T entity) => _context.Set<T>().AddAsync(entity);

        public async ValueTask<T> CreateAsync(T entity) => (await _context.Set<T>().AddAsync(entity).ConfigureAwait(false)).Entity;

        public async ValueTask<M> CreateAsync<M>(T entity) => _mapper.Map<M>(await CreateAsync(entity).ConfigureAwait(false));

        public async ValueTask<M> CreateAsync<M>(object data) => _mapper.Map<M>(await CreateAsync<T>(data).ConfigureAwait(false));

        #endregion ASYNC

        #endregion CREATE

        #region RETRIEVE

        /// <summary>
        /// Retrieve <see cref="T"/> by <see cref="PK"/>
        /// </summary>
        /// <param name="id">Primary Key</param>
        public T Retrieve(PK id) => _context.Set<T>().Single(s => s.Id.Equals(id));

        #endregion RETRIEVE

        #region UPDATE

        public T Update(PK id, T entity)
        {
            entity.Id = id;
            return Update(entity);
        }

        public T Update(PK id, object data) => Update(id, _mapper.Map<T>(data));

        public T Update(object data) => _context.Set<T>().Update(_mapper.Map<T>(data)).Entity;

        public T Update(T entity) => _context.Set<T>().Update(entity).Entity;

        public M Update<M>(PK id, T data) => _mapper.Map<M>(Update(id, data));

        public M Update<M>(PK id, object data) => _mapper.Map<M>(Update(id, data));

        public M Update<M>(T data) => _mapper.Map<M>(Update(data));

        #endregion UPDATE

        #region DELETE

        public T Delete(T entity)
        {
            return _context.Set<T>().Remove(entity).Entity;
        }

        public T Delete(PK id)
        {
            return _context.Set<T>().Remove(new T { Id = id }).Entity;
        }

        #endregion DELETE
    }
}