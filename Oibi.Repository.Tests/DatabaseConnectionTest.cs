using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Oibi.Repository.Demo.Models;
using Oibi.Repository.Demo.Repositories;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Oibi.Repository.Tests;

public class DatabaseConnectionTest : IClassFixture<TestContainerApplicationFactory>
{
    private readonly TestContainerApplicationFactory _testFixure;
    private readonly IServiceScope _scope;

    private readonly LibraryDbScope _libraryScope;
    private readonly LibraryContext _context;

    public DatabaseConnectionTest(TestContainerApplicationFactory testFixure)
    {
        _testFixure = testFixure;
        _scope = _testFixure.Server.Services.CreateScope();

        _libraryScope = _scope.ServiceProvider.GetRequiredService<LibraryDbScope>();
        _context = _scope.ServiceProvider.GetRequiredService<LibraryContext>();
    }

    [Fact]
    public async Task TimeZone_UTC()
    {
        var results = await _context.Database.SqlQueryRaw<string>("SHOW timezone;").ToListAsync();
        Assert.Equal("UTC", results[0]);
    }

    [Fact]
    public async Task TestDatabaseConnection()
    {
        Exception connectionException = null;

        try
        {
            // Tentativo di eseguire una semplice operazione di lettura dal database.
            var count = await _libraryScope.BookRepository.CountAsync();
            Assert.True(count >= 0, "Connessione al database riuscita e conteggio libri ottenuto.");
        }
        catch (Exception ex)
        {
            connectionException = ex;
            Assert.Fail(ex.Message);
        }

        Assert.Null(connectionException); // Assicurati che non ci siano eccezioni, indicando problemi di connessione.
    }


    [Theory]
    [InlineData(0)]  // UTC
    [InlineData(1)]  // CET (Central European Time, UTC+1)
    [InlineData(-8)] // PST (Pacific Standard Time, UTC-8)
    public void Book_Timestamps_ShouldBeTimeZoneAgnostic(int offsetHours)
    {
        // Arrange
        var currentTimeZoneOffset = TimeSpan.FromHours(offsetHours);
        var currentTime = new DateTimeOffset(DateTime.UtcNow.Ticks, currentTimeZoneOffset);

        var book = new Book
        {
            Title = "Timezone Test Book",
            Isbn = "1234567890123",
            ArrivedAt = currentTime,
            //CreatedAt = currentTime,
            //UpdatedAt = currentTime
        };

        // Act
        _context.Books.Add(book);
        _context.SaveChanges();

        // Clear the tracker to get fresh data from the database
        _context.ChangeTracker.Clear();

        var retrievedBook = _context.Books.AsNoTracking().FirstOrDefault(b => b.Id == book.Id);

        // Assert
        Assert.NotNull(retrievedBook);
        // We expect the DateTimeOffset to be converted to UTC, so let's compare the UTC DateTime values
        Assert.Equal(currentTime.UtcDateTime, book.ArrivedAt);
        // postgre preciso al millisecondo non al ticks
        Assert.True((retrievedBook.ArrivedAt - currentTime.UtcDateTime).Duration() < TimeSpan.FromMilliseconds(1));

        //Assert.Equal(currentTime.UtcDateTime, retrievedBook.ArrivedAt);
        //Assert.Equal(currentTime.UtcDateTime, retrievedBook.CreatedAt);
        //Assert.Equal(currentTime.UtcDateTime, retrievedBook.UpdatedAt);

        // Clean up
        _context.Books.Remove(retrievedBook);
        _context.SaveChanges();
    }

    [Fact]
    public async Task Try_DateTimeBorders()
    {
        var book = new Book
        {
            Title = "Timezone Test Book",
            Isbn = "1234567890123",
            ArrivedAt = new DateTimeOffset(DateTime.MaxValue, TimeSpan.Zero),
        };

        _context.Books.Add(book);
        await _context.SaveChangesAsync();



        var book2 = new Book
        {
            Title = "Timezone Test Book 2",
            Isbn = "1234567890120",
            ArrivedAt = new DateTimeOffset(DateTime.MinValue, TimeSpan.Zero),
        };

        _context.Books.Add(book2);
        await _context.SaveChangesAsync();
    }
}