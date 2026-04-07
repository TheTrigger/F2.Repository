using F2.Repository.Abstracts;
using F2.Repository.Demo.Repositories;

namespace F2.Repository.Demo;

/// <summary>
/// Repositories scope
/// </summary>
public class LibraryDbScope : DbContextScope<LibraryContext>
{
    public LibraryDbScope(LibraryContext context, BookRepository bookRepository, AuthorRepository authorRepository) : base(context)
    {
        BookRepository = bookRepository;
        AuthorRepository = authorRepository;
    }

    public BookRepository BookRepository { get; }
    public AuthorRepository AuthorRepository { get; }
}