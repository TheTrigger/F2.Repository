using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;

namespace Oibi.Repository.Demo.Models
{
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
            var authorId1 = Guid.NewGuid();
            var authorId2 = Guid.Parse("CEC0FB8E-4935-4E85-A271-2A2814D632CD");

            modelBuilder.Entity<Author>().HasData(
                new Author { Id = authorId1, Name = "William Shakespeare" },
                new Author { Id = authorId2, Name = "Oibi.dev" }
            );

            var book1Id = Guid.NewGuid();
            var book2Id = Guid.Parse("80D521C9-431A-4324-B078-8D25E9D2DA4C");

            modelBuilder.Entity<Book>().HasData(
                new Book { Id = book1Id, Title = "Hamlet", Isbn = "1234567890123" },
                new Book { Id = book2Id, Title = "King Lear", Isbn = "0987654321045" },
                new Book { Id = Guid.NewGuid(), Title = "Random Othello w/ no authors" }
            );

            modelBuilder.Entity<BookAuthors>().HasData(
                new BookAuthors { AuthorId = authorId1, BookId = book1Id },
                new BookAuthors { AuthorId = authorId1, BookId = book2Id },
                new BookAuthors { AuthorId = authorId2, BookId = book2Id }
            );
        }
    }
}