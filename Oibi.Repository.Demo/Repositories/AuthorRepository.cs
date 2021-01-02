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
        public AuthorRepository(LibraryContext repositoryContext) : base(repositoryContext)
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
            return default;
        }
    }
}