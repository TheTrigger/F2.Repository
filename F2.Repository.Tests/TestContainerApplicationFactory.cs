using DotNet.Testcontainers.Builders;
using F2.Repository.Demo;
using F2.Testing;
using Microsoft.AspNetCore.Hosting;
using Testcontainers.PostgreSql;

namespace F2.Repository.Tests;

public class TestContainerApplicationFactory : ServerFixture<Startup>
{
    private const ushort _port = 5432;

    private readonly PostgreSqlContainer _applicationDatabase;

    private readonly PostgreSqlBuilder _databaseContainerBuilder = new PostgreSqlBuilder("postgres:16-alpine")
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
        builder.UseSetting("ConnectionStrings:Demo", _applicationDatabase.GetConnectionString());
        base.ConfigureWebHost(builder);
    }

    public override async ValueTask InitializeAsync()
    {
        await _applicationDatabase.StartAsync().ConfigureAwait(false);
        await base.InitializeAsync().ConfigureAwait(false);
    }

    public override async ValueTask DisposeAsync()
    {
        await _applicationDatabase.StopAsync().ConfigureAwait(false);
        await base.DisposeAsync().ConfigureAwait(false);

        GC.SuppressFinalize(this);
    }
}
