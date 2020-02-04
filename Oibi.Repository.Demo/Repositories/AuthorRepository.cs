using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Oibi.Repository.Abstracts;
using Oibi.Repository.Demo.Models;

namespace Oibi.Repository.Demo.Repositories
{
    public class AuthorRepository : GenericEntityRepository<Author>
    {
        public AuthorRepository(DbContext repositoryContext, IMapper mapper) : base(repositoryContext, mapper)
        {
        }
    }
}