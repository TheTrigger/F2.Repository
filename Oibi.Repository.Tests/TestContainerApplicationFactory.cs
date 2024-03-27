using DotNet.Testcontainers.Builders;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Oibi.Repository.Demo;
using Oibi.Repository.Demo.Models;
using Oibi.TestHelper;
using System.Linq;
using System.Threading.Tasks;
using Testcontainers.PostgreSql;
using Xunit;

namespace Oibi.Repository.Tests;

public class TestContainerApplicationFactory : ServerFixture<Startup>, IAsyncLifetime
{
    private const ushort port = 5432;

    private readonly PostgreSqlContainer _applicationDatabase;

    private readonly PostgreSqlBuilder _databaseContainerBuilder = new PostgreSqlBuilder()
        .WithImage("postgres:16-alpine")
        .WithUsername("test")
        .WithPassword("test")
        .WithDatabase("test")
        .WithPortBinding(port, true)
        .WithCleanUp(true)
        .WithWaitStrategy(Wait.ForUnixContainer().UntilPortIsAvailable(port))
    ;

    public TestContainerApplicationFactory()
    {
        _applicationDatabase = _databaseContainerBuilder.Build();
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureTestServices(services =>
        {
            var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<LibraryContext>));
            services?.Remove(descriptor);
            services.AddDbContext<LibraryContext>(options =>
            {
                options.UseNpgsql(_applicationDatabase.GetConnectionString(),
                    npgsqlOptions => npgsqlOptions.MigrationsAssembly(typeof(LibraryContext).Assembly.FullName))
                    .EnableSensitiveDataLogging()
                    .EnableDetailedErrors()
                    ;

            }, ServiceLifetime.Scoped);
        });
    }

    Task IAsyncLifetime.InitializeAsync()
    {
        return _applicationDatabase.StartAsync();
    }


    async Task IAsyncLifetime.DisposeAsync()
    {
        await _applicationDatabase.StopAsync().ConfigureAwait(false);

        await base.DisposeAsync().ConfigureAwait(false);
    }
}
