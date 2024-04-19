using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Oibi.Repository.Extensions;

namespace Oibi.Repository.Demo.Models;

public class LibraryContext : DbContext
{
	private readonly ILoggerFactory _loggerFactory;

	public LibraryContext(ILoggerFactory loggerFactory, DbContextOptions options) : base(options)
	{
		_loggerFactory = loggerFactory;
	}

	public DbSet<Author> Authors { get; set; }
	public DbSet<Book> Books { get; set; }

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		base.OnConfiguring(optionsBuilder);

		optionsBuilder.UseLoggerFactory(_loggerFactory);
	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
        //modelBuilder.ApplyConfiguration(new BookEntityConfiguration());
        //modelBuilder.ApplyConfiguration(new AuthorEntityConfiguration());
		modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);

        modelBuilder.UseUtcDateTimeOffset(typeof(Author));
    }
}