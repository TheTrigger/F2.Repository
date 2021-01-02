using AutoMapper;
using Oibi.Repository.Abstracts;
using Oibi.Repository.Demo.Models;

namespace Oibi.Repository.Demo.Repositories
{
    public class BookRepository : GenericEntityRepository<Book>
    {
        public BookRepository(LibraryContext context) : base(context)
        {
        }
    }
}