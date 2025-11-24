using DotNet.Testcontainers.Builders;
using F2.Repository.Demo;
using F2.Testing;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;
using Testcontainers.PostgreSql;

namespace F2.Repository.Tests;

public class TestContainerApplicationFactory : ServerFixture<Startup>
{
    private const ushort _port = 5432;

    private readonly PostgreSqlContainer _applicationDatabase;

    private readonly PostgreSqlBuilder _databaseContainerBuilder = new PostgreSqlBuilder()
        .WithImage("postgres:16-alpine")
        .WithUsername("test")
        .WithPassword("test")
        .WithDatabase("test")
        .WithPortBinding(_port, true)
        .WithCleanUp(true)
        .WithWaitStrategy(Wait.ForUnixContainer().UntilInternalTcpPortIsAvailable(_port))
    ;

    public TestContainerApplicationFactory()
    {
        _applicationDatabase = _databaseContainerBuilder.Build();
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        base.ConfigureWebHost(builder);
        builder
            .ConfigureAppConfiguration((hostingContext, config) =>
            {
                var newConfigs = new Dictionary<string, string>
                {
                    { "ConnectionStrings:Demo", _applicationDatabase.GetConnectionString() }
                };

                config.AddInMemoryCollection(newConfigs);
            })
            .ConfigureTestServices(services =>
            {
                //var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<LibraryContext>));
                //services?.Remove(descriptor);
                //services.AddDbContext<LibraryContext>(options =>
                //{
                //    // connection string from container
                //    options.UseNpgsql(_applicationDatabase.GetConnectionString(),
                //        npgsqlOptions => npgsqlOptions.MigrationsAssembly(typeof(LibraryContext).Assembly.FullName))
                //        .EnableSensitiveDataLogging(true)
                //        .EnableDetailedErrors()
                //        ;

                //}, ServiceLifetime.Scoped);
            })

        ;
    }

    public override async Task InitializeAsync()
    {
        await _applicationDatabase.StartAsync().ConfigureAwait(false);
        await base.InitializeAsync().ConfigureAwait(false);
    }

    public async override ValueTask DisposeAsync()
    {
        await _applicationDatabase.StopAsync().ConfigureAwait(false);
        await base.DisposeAsync().ConfigureAwait(false);
    }
}
