using Oibi.Repository.Abstracts;
using Oibi.Repository.Demo.Models;

namespace Oibi.Repository.Demo.Repositories
{
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
}