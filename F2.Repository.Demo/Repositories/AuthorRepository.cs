using F2.Repository.Abstracts;

namespace F2.Repository.Demo.Repositories
{
    public class AuthorRepository : GenericEntityRepository<Author>
    {
        public AuthorRepository(LibraryContext repositoryContext) : base(repositoryContext)
        {
        }
    }
}