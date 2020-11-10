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
		private readonly CancellationToken _cancellationToken = new CancellationToken();

		protected readonly DbContext _context;
		protected readonly IMapper _mapper;

		#region AS QUERYABLE

		/// <summary>
		/// <inheritdoc/>
		/// </summary>
		protected IQueryable<TEntity> Queryable => Set.AsQueryable();

		/// <summary>
		/// <inheritdoc/>
		/// </summary>
		public Type ElementType => typeof(TEntity);

		/// <summary>
		/// <inheritdoc/>
		/// </summary>
		public Expression Expression => Queryable.Expression;

		/// <summary>
		/// <inheritdoc/>
		/// </summary>
		public IQueryProvider Provider => Queryable.Provider;

		/// <summary>
		/// <inheritdoc/>
		/// </summary>
		/// <returns><inheritdoc/></returns>
		public IEnumerator GetEnumerator() => Queryable.GetEnumerator();

		/// <summary>
		/// <inheritdoc/>
		/// </summary>
		/// <returns><inheritdoc/></returns>
		IEnumerator<TEntity> IEnumerable<TEntity>.GetEnumerator() => Queryable.GetEnumerator();

		/// <summary>
		/// <inheritdoc/>
		/// </summary>
		/// <param name="cancellationToken"><inheritdoc/></param>
		/// <returns><inheritdoc/></returns>
		public IAsyncEnumerator<TEntity> GetAsyncEnumerator(CancellationToken cancellationToken = default)
			=> Set.AsAsyncEnumerable().GetAsyncEnumerator(cancellationToken);

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

		//public ValueTask<EntityEntry<TEntity>> CreateAsync(TEntity entity) => _context.Set<TEntity>().AddAsync(entity);

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

		public IQueryable<TDestMap> ProjectTo<TDestMap>() => Set.ProjectTo<TDestMap>(_mapper.ConfigurationProvider);

		/// <inheritdoc cref="DbContext.SaveChanges"/>
		public Task<int> SaveChangesAsync() => _context.SaveChangesAsync(_cancellationToken);

		/// <inheritdoc cref="DbContext.SaveChangesAsync(bool, CancellationToken)"/>
		public Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess) => _context.SaveChangesAsync(acceptAllChangesOnSuccess, _cancellationToken);

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