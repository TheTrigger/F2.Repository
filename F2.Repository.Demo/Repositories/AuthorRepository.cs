using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using F2.Repository.Abstracts;
using F2.Repository.Demo.Models;
using System.Collections.Generic;

namespace F2.Repository.Demo.Repositories
{
    public class AuthorRepository : GenericEntityRepository<Author>
    {
        public AuthorRepository(LibraryContext repositoryContext) : base(repositoryContext)
        {
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