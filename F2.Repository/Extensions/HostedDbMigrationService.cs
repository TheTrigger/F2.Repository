using Microsoft.EntityFrameworkCore;

namespace F2.Repository.Extensions;

public static class DatabaseMigrationExtensions
{
    public static IServiceCollection AddDatabaseMigration<TContext>(this IServiceCollection services)
        where TContext : DbContext
    {
        services.AddHostedService<HostedDbMigrationService<TContext>>();

        return services;
    }
}

public class HostedDbMigrationService<TContext> : IHostedService
    where TContext : DbContext
{
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly ILogger<HostedDbMigrationService<TContext>> _logger;

    public HostedDbMigrationService(IServiceScopeFactory scopeFactory, ILogger<HostedDbMigrationService<TContext>> logger)
    {
        _scopeFactory = scopeFactory;
        _logger = logger;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        _logger.LogDebug("Starting Migration {Type}", typeof(TContext));
        using var scope = _scopeFactory.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<TContext>();
        await context.Database.MigrateAsync(cancellationToken).ConfigureAwait(false);
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}
