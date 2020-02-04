using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Oibi.Repository.Abstracts;
using Oibi.Repository.Demo.Models;

namespace Oibi.Repository.Demo.Repositories
{
    public class BookRepository : GenericEntityRepository<Book>
    {
        public BookRepository(DbContext repositoryContext, IMapper mapper) : base(repositoryContext, mapper)
        {
        }
    }
}