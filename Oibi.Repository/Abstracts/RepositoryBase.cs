using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Oibi.Repository.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Oibi.Repository.Abstracts
{
    public abstract class RepositoryBase<T> : IRepository<T> where T : class, new()
    {
        protected readonly DbContext _context;
        protected readonly IMapper _mapper;

        #region AS QUERYABLE

        private DbSet<T> _set;
        protected DbSet<T> Set => _set ??= _context.Set<T>();
        protected IQueryable<T> Queryable => Set.AsQueryable();

        public Type ElementType => typeof(T);
        public Expression Expression => Queryable.Expression;
        public IQueryProvider Provider => Queryable.Provider;

        IEnumerator IEnumerable.GetEnumerator() => Queryable.GetEnumerator();

        public IEnumerator GetEnumerator() => Queryable.GetEnumerator();

        IEnumerator<T> IEnumerable<T>.GetEnumerator() => Queryable.GetEnumerator();

        #endregion AS QUERYABLE

        protected RepositoryBase(DbContext repositoryContext, IMapper mapper)
        {
            _context = repositoryContext;
            _mapper = mapper;
        }

        #region CREATE

        public T Create(T entity) => Set.Add(entity).Entity;

        /// <summary>
        /// Using <see cref="AutoMapper"/> to create <see cref="T"/> resource
        /// </summary>
        /// <param name="data">An <see cref="object"/> mappable to <see cref="T"/></param>
        public T Create(object data) => Set.Add(_mapper.Map<T>(data)).Entity;

        /// <summary>
        /// Create a new entity from a dto and returns a dto using <see cref="AutoMapper"/>
        /// </summary>
        /// <typeparam name="MAP"></typeparam>
        /// <param name="data"></param>
        public MAP Create<MAP>(object data) => _mapper.Map<MAP>(Create(data));

        #region ASYNC

        //public ValueTask<EntityEntry<T>> CreateAsync(T entity) => _context.Set<T>().AddAsync(entity);

        //public async ValueTask<T> CreateAsync(T entity) => (await Set.AddAsync(entity).ConfigureAwait(false)).Entity;

        //public async ValueTask<MAP> CreateAsync<MAP>(T entity) => _mapper.Map<MAP>(await CreateAsync(entity).ConfigureAwait(false));

        //public async ValueTask<MAP> CreateAsync<MAP>(object data) => _mapper.Map<MAP>(await CreateAsync<T>(data).ConfigureAwait(false));

        #endregion ASYNC

        #endregion CREATE

        #region RETRIEVE

        // nothing here..

        #endregion RETRIEVE

        #region UPDATE

        public T Update(object data) => Set.Update(_mapper.Map<T>(data)).Entity;

        public T Update(T entity) => Set.Update(entity).Entity;

        public MAP Update<MAP>(T data) => _mapper.Map<MAP>(Update(data));

        #endregion UPDATE

        #region DELETE

        public T Delete(T entity) => Set.Remove(entity).Entity;

        #endregion DELETE
    }
}