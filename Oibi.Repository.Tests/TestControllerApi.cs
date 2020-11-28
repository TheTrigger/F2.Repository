using Microsoft.EntityFrameworkCore;
using Oibi.Repository.Demo;
using Oibi.Repository.Demo.Mapper.Dto;
using Oibi.Repository.Demo.Repositories;
using Oibi.TestHelper;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Oibi.Repository.Tests
{
    public class TestControllerApi : IClassFixture<ServerFixture<Startup>>
    {
        private readonly ServerFixture<Startup> _serverFixture;
        private readonly AuthorRepository _authorRepository;

        public TestControllerApi(ServerFixture<Startup> serverFixture)
        {
            _serverFixture = serverFixture;
            _authorRepository = _serverFixture.GetService<AuthorRepository>();
        }

        [Fact]
        public async Task TestLibrary()
        {
            var results = await _serverFixture.GetAsync<IEnumerable<AuthorDto>>("/api/library");

            Assert.NotEmpty(results);

            var firstElemnt = results.First();

            Assert.NotEmpty(firstElemnt.Name);
            Assert.NotEmpty(firstElemnt.Books);
            Assert.NotEmpty(firstElemnt.Books.First().Title);
        }

        [Fact]
        public async Task ToListAsync()
		{
            var list =  await _authorRepository.ToListAsync();
            Assert.NotNull(list);
        }
    }
}
