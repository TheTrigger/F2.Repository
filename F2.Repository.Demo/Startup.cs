using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using F2.Repository.Demo.Mapper;
using F2.Repository.Demo.Models;
using F2.Repository.Extensions;

namespace F2.Repository.Demo;

public class Startup(IConfiguration configuration)
{
    private readonly IConfiguration _configuration = configuration;

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddHostedService<HostedDbMigrationService<LibraryContext>>();

        var connectionString = _configuration.GetConnectionString("Demo");
        services.AddDbContext<LibraryContext>(config => config.UseNpgsql(connectionString));
        services.AddDatabaseScope<LibraryDbScope>();

        // https://github.com/AutoMapper/AutoMapper.Extensions.Microsoft.DependencyInjection
        services.AddAutoMapper(typeof(MappingProfile));
        services.AddControllers();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public static void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        //app.UseDatabaseMigration<LibraryContext>(); // relational databases only

        app.UseRouting();
        app.UseEndpoints(endpoints => endpoints.MapControllers());
    }
}