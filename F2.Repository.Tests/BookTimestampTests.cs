using F2.Repository.Demo.Models;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace F2.Repository.Tests;

public class BookTimestampTests : IClassFixture<TestContainerApplicationFactory>
{
    private readonly LibraryContext _context;

    public BookTimestampTests(TestContainerApplicationFactory testFixture)
    {
        var scope = testFixture.Server.Services.CreateScope();
        _context = scope.ServiceProvider.GetRequiredService<LibraryContext>();
    }
}
