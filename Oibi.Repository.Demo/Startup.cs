using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Oibi.Repository.Demo.Mapper;
using Oibi.Repository.Demo.Models;
using Oibi.Repository.Demo.Repositories;

namespace Oibi.Repository.Demo
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private readonly IConfiguration _configuration;

        // This method gets called by the runtime. Use this method to add services to the container.
        public static void ConfigureServices(IServiceCollection services)
        {
            // package Microsoft.EntityFrameworkCore.InMemory
            services.AddDbContext<LibraryContext>(config => config.UseInMemoryDatabase(nameof(LibraryContext)));

            // https://github.com/AutoMapper/AutoMapper.Extensions.Microsoft.DependencyInjection
            services.AddAutoMapper(typeof(MappingProfile));

            services.AddScoped<AuthorRepository>();
            services.AddScoped<BookRepository>();

            services.AddControllers(); //.AddJsonOptions(option => { });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public static void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => endpoints.MapControllers());
        }
    }
}