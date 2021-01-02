using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Oibi.Repository.Demo.Mapper;
using Oibi.Repository.Demo.Models;
using Oibi.Repository.Demo.Repositories;
using Oibi.Repository.Extensions;

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
		public void ConfigureServices(IServiceCollection services)
		{
			// package Microsoft.EntityFrameworkCore.InMemory
			// services.AddDbContext<LibraryContext>(config => config.UseInMemoryDatabase(nameof(LibraryContext)));
			//services.AddDbContext<LibraryContext>(config => config.UseSqlServer(_configuration.GetConnectionString("Demo")));
			services.AddDbContext<LibraryContext>(config => config.UseInMemoryDatabase(nameof(LibraryContext)));

			// https://github.com/AutoMapper/AutoMapper.Extensions.Microsoft.DependencyInjection
			services.AddAutoMapper(typeof(MappingProfile));
			services.AddControllers();

			services.AddDatabaseScope<LibraryDbScope>();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public static void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			app.UseRouting();
			app.UseEndpoints(endpoints => endpoints.MapControllers());
		}
	}
}