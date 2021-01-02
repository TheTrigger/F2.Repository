using System.Threading;
using System.Threading.Tasks;

namespace Oibi.Repository.Interfaces
{
	/// <summary>
	/// Alternative to IUnitOfWork
	/// </summary>
	public interface IDbContextScope
	{
		/// <inheritdoc cref="Microsoft.EntityFrameworkCore.DbContext.SaveChangesAsync(CancellationToken)"/>
		Task<int> SaveChangesAsync(CancellationToken cancellationToken);

		/// <inheritdoc cref="Microsoft.EntityFrameworkCore.DbContext.SaveChangesAsync(bool, CancellationToken)"/>
		Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken);
	}
}