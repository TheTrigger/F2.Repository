using Microsoft.EntityFrameworkCore;
using Oibi.Repository.Demo;
using Oibi.Repository.Demo.Repositories;
using Oibi.TestHelper;
using System.Threading.Tasks;
using Xunit;

namespace Oibi.Repository.Tests
{
	public class TestControllerApi : IClassFixture<ServerFixture<Startup>>
	{
		private readonly ServerFixture<Startup> _serverFixture;
		private readonly LibraryDbScope _libraryScope;

		public TestControllerApi(ServerFixture<Startup> serverFixture)
		{
			_serverFixture = serverFixture;
			_libraryScope = _serverFixture.GetService<LibraryDbScope>();
		}

		[Fact]
		public async Task TestLibrary()
		{
			var results = await _libraryScope.AuthorRepository.ToListAsync();

			Assert.NotEmpty(results);

			foreach (var author in results)
			{
				Assert.NotEmpty(author.Name);
				Assert.NotEmpty(author.BookAuthors);
			}
		}

		[Fact]
		public async Task ToListAsync()
		{
			var results = await _libraryScope.BookRepository.ToListAsync();
			Assert.NotNull(results);
		}
	}
}