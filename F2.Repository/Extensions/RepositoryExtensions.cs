using Microsoft.Extensions.DependencyInjection;
using F2.Repository.Interfaces;
using System.Linq;

namespace F2.Repository.Extensions
{
	public static class RepositoryExtensions
	{
		/// <summary>
		/// Register the current <typeparamref name="TDbContextScope"/> and repositories (from properties) as <see cref="IServiceCollection.AddScoped()"/>
		/// </summary>
		/// <typeparam name="TDbContextScope"></typeparam>
		/// <param name="services"><inheritdoc cref="IServiceCollection"/></param>
		/// <returns><inheritdoc cref="IServiceCollection"/></returns>
		public static IServiceCollection AddDatabaseScope<TDbContextScope>(this IServiceCollection services)
			where TDbContextScope : class, IDbContextScope
		{
			var repositories = typeof(TDbContextScope).GetProperties()
				.Where(w => typeof(IRepository).IsAssignableFrom(w.PropertyType));

			services.AddScoped<TDbContextScope>();
			foreach (var repoType in repositories)
			{
				services.AddScoped(repoType.PropertyType);
			}

			return services;
		}
	}
}