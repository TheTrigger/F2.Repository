using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Reflection;

namespace Oibi.Repository.Extensions;

public static class ModelBuilderExtensions
{
	/// <summary>
	/// Auto find and apply all IEntityTypeConfiguration to modelBuilder
	/// modelBuilder.ApplyConfigurationsFromAssembly(typeof(YourDbContext).Assembly);
	/// </summary>
	[Obsolete("See modelBuilder.ApplyConfigurationsFromAssembly")]
	public static void ApplyAllConfigurations<TDbContext>(this ModelBuilder modelBuilder)
		where TDbContext : DbContext
	{
		var applyConfigurationMethodInfo = modelBuilder.GetType().GetMethods(BindingFlags.Instance | BindingFlags.Public)
			.First(m => m.Name.Equals(nameof(ModelBuilder.ApplyConfiguration), StringComparison.OrdinalIgnoreCase));

		var ret = typeof(TDbContext).Assembly.GetTypes()
			.Select(t => (t,
				i: t.GetInterfaces().FirstOrDefault(i =>
					i.Name.Equals(typeof(IEntityTypeConfiguration<>).Name, StringComparison.Ordinal))))
			.Where(it => it.i is not null)
			.Select(it => (et: it.i.GetGenericArguments()[0], cfgObj: Activator.CreateInstance(it.t)))
			.Select(it => applyConfigurationMethodInfo.MakeGenericMethod(it.et).Invoke(modelBuilder, new[] { it.cfgObj }))
		.ToList();
	}
}