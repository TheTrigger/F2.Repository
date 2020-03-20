using Oibi.Repository.Demo;
using Oibi.Repository.Demo.Mapper.Dto;
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

        public TestControllerApi(ServerFixture<Startup> serverFixture)
        {
            _serverFixture = serverFixture;
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
    }
}
