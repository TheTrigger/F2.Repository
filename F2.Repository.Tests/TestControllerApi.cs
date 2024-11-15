using F2.Repository.Demo;
using F2.Repository.Demo.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using Xunit;

namespace F2.Repository.Tests;

public class TestControllerApi : IClassFixture<TestContainerApplicationFactory>
{
    private readonly TestContainerApplicationFactory _serverFixture;
    private readonly IServiceScope _scope;
    private readonly LibraryDbScope _libraryScope;
    public TestControllerApi(TestContainerApplicationFactory serverFixture)
    {
        _serverFixture = serverFixture;
        _scope = _serverFixture.Server.Services.CreateScope();
        _libraryScope = _scope.ServiceProvider.GetRequiredService<LibraryDbScope>();
    }

    [Fact]
    public async Task CreateExampleDataIfNotExists()
    {
        var a1 = new Author { Name = "William Shakespeare" };
        var a2 = new Author { Name = "Oibi.dev" };

        _libraryScope.AuthorRepository.Create(a1);
        _libraryScope.AuthorRepository.Create(a2);

        var affectedRows = await _libraryScope.SaveChangesAsync();
        Assert.NotEqual(Guid.Empty, a1.Id);
        Assert.NotEqual(default, affectedRows);

        var b1 = new Book { Title = "Hamlet", Isbn = "1234567890123" };
        var b2 = new Book { Title = "King Lear", Isbn = "0987654321045" };
        var b3 = new Book { Id = Guid.NewGuid(), Title = "Random Othello w/ no authors" };

        _libraryScope.BookRepository.Create(b1);
        _libraryScope.BookRepository.Create(b2);
        _libraryScope.BookRepository.Create(b3);

        a1.Books = [b1, b2];
        a2.Books = [b2, b3];

        affectedRows = await _libraryScope.SaveChangesAsync();
        Assert.NotEqual(default, affectedRows);

        var results = await _libraryScope.AuthorRepository
                                            .Include(i => i.Books)
                                        .ToListAsync();

        Assert.NotEmpty(results);
        foreach (var author in results)
        {
            Assert.NotEmpty(author.Name);
            Assert.NotEmpty(author.Books);
        }
    }

    [Fact]
    public async Task ToListAsync()
    {
        var b1 = new Book { Title = "Hamlet", Isbn = "1234567890123" };
        _libraryScope.BookRepository.Create(b1);
        var affectedRows = await _libraryScope.SaveChangesAsync();
        Assert.Equal(1, affectedRows);

        var results = await _libraryScope.BookRepository.ToListAsync();
        Assert.NotNull(results);
    }

    [Fact]
    public async Task CanCreateAndDelete()
    {
        var a1 = new Author { Name = "William Shakespeare" };
        _libraryScope.AuthorRepository.Create(a1);
        await _libraryScope.SaveChangesAsync(default);

        _libraryScope.AuthorRepository.RemoveRange(a1);
        await _libraryScope.SaveChangesAsync(default);

        Assert.NotEqual(Guid.Empty, a1.Id);
    }
}