using F2.Repository.Abstracts;
using F2.Repository.Demo.Models;

namespace F2.Repository.Demo.Repositories
{
    public class BookRepository : GenericEntityRepository<Book>
    {
        public BookRepository(LibraryContext context) : base(context)
        {
        }
    }
}