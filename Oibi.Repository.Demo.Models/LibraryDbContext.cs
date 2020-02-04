using Microsoft.EntityFrameworkCore;
using System;

namespace Oibi.Repository.Demo.Models
{
    public class LibraryDbContext : DbContext
    {
        public LibraryDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var authorId1 = Guid.Parse("5D2B7693-A104-4D26-9D1E-88B8936C234B");
            var authorId2 = Guid.Parse("CEC0FB8E-4935-4E85-A271-2A2814D632CD");

            modelBuilder.Entity<Author>().HasData(
                new Author { Id = authorId1, Name = "William Shakespeare" },
                new Author { Id = authorId2, Name = "Oibi.dev" }
            );

            var book1Id = Guid.Parse("BF87E433-B4D6-4F7D-96EC-6B8AB80F43BE");
            var book2Id = Guid.Parse("80D521C9-431A-4324-B078-8D25E9D2DA4C");

            modelBuilder.Entity<Book>().HasData(
                new Book { Id = book1Id, Title = "Hamlet" },
                new Book { Id = book2Id, Title = "King Lear" },
                new Book { Title = "Random Othello w/ no authors" }
            );

            modelBuilder.Entity<BookAuthors>().HasData(
                new BookAuthors { AuthorId = authorId1, BookId = book1Id },
                new BookAuthors { AuthorId = authorId1, BookId = book2Id },
                new BookAuthors { AuthorId = authorId2, BookId = book2Id }
            );
        }
    }
}