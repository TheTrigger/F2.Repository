using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Oibi.Repository.Abstracts;
using Oibi.Repository.Demo.Models;
using System.Collections.Generic;

namespace Oibi.Repository.Demo.Repositories
{
    public class AuthorRepository : GenericEntityRepository<Author>
    {
        public AuthorRepository(LibraryContext repositoryContext, IMapper mapper) : base(repositoryContext, mapper)
        {
            _context.Database.EnsureCreated(); // test seeding purpose
        }

        /// <summary>
        /// Get all Authors and books
        /// </summary>
        public IEnumerable<Author> GetAuthorsAndBooks() => GetAuthorsAndBooks<Author>();

        /// <summary>
        /// Get all authors and books. Mapped to destination
        /// </summary>
        public IEnumerable<TDestMap> GetAuthorsAndBooks<TDestMap>()
        {
            var query = this.Include(i => i.BookAuthors).ThenInclude(i => i.Book)
                .ProjectTo<TDestMap>(_mapper.ConfigurationProvider);

            return _mapper.Map<IEnumerable<TDestMap>>(query);
        }
    }
}