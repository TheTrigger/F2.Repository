using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace F2.Repository.Extensions
{
	public static class ApplicationBuilderExtensions
	{
		/// <summary>
		/// Automatically check migration is up to date. Only relational database is supported. <see cref="Microsoft.EntityFrameworkCore.Relational"/>
		/// </summary>
		/// <typeparam name="TDbContext"></typeparam>
		/// <param name="applicationBuilder"></param>
		public static IApplicationBuilder UseDatabaseMigration<TDbContext>(this IApplicationBuilder applicationBuilder) where TDbContext : DbContext
		{
			// UseAutoMigrateDatabase?
			using var serviceScope = applicationBuilder.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();
			var context = serviceScope.ServiceProvider.GetService<TDbContext>();
			context.Database.Migrate();

			return applicationBuilder;
		}
	}
}