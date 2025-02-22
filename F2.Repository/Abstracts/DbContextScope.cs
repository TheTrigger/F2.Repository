﻿using F2.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace F2.Repository.Abstracts;

/// <inheritdoc cref="IDbContextScope"/>
/// <typeparam name="TDbContext">Your database type context</typeparam>
public abstract class DbContextScope<TDbContext> : IDbContextScope
	where TDbContext : DbContext
{
	/// <summary>
	/// <inheritdoc cref="DbContext"/>
	/// </summary>
	protected readonly TDbContext _context;

	/// <summary>
	/// <inheritdoc cref="DbContext()"/>
	/// </summary>
	/// <param name="context"><inheritdoc/></param>
	protected DbContextScope(TDbContext context)
	{
		_context = context;
	}

	/// <summary>
	/// <inheritdoc/>
	/// </summary>
	/// <param name="cancellationToken"><inheritdoc/></param>
	/// <returns><inheritdoc/></returns>
	public virtual Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) => _context.SaveChangesAsync(cancellationToken);

	/// <summary>
	/// <inheritdoc/>
	/// </summary>
	/// <param name="acceptAllChangesOnSuccess"><inheritdoc/></param>
	/// <param name="cancellationToken"><inheritdoc/></param>
	/// <returns><inheritdoc/></returns>
	public virtual Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
		=> _context.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
}