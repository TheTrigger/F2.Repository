using System.Threading;
using System.Threading.Tasks;

namespace F2.Repository.Interfaces
{
	/// <summary>
	/// Alternative to IUnitOfWork
	/// </summary>
	public interface IDbContextScope
	{
		/// <inheritdoc cref="Microsoft.EntityFrameworkCore.DbContext.SaveChangesAsync(CancellationToken)"/>
		Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

		/// <inheritdoc cref="Microsoft.EntityFrameworkCore.DbContext.SaveChangesAsync(bool, CancellationToken)"/>
		Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default);
	}
}