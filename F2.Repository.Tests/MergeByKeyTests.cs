using F2.Repository.Demo.Models;
using F2.Repository.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace F2.Repository.Tests;

public class MergeByKeyTests : IClassFixture<TestContainerApplicationFactory>
{
    private readonly LibraryContext _context;

    public MergeByKeyTests(TestContainerApplicationFactory testFixture)
    {
        var scope = testFixture.Server.Services.CreateScope();
        _context = scope.ServiceProvider.GetRequiredService<LibraryContext>();
    }

    [Fact]
    public async Task MergeByKey_ShouldInsertUpdateDeleteBooks()
    {
        // Arrange: Clear database and seed initial data
        await _context.Books.ExecuteDeleteAsync();
        await _context.Publishers.ExecuteDeleteAsync();

        var publisher = new Publisher
        {
            Id = Guid.NewGuid(),
            Name = "Test Publisher",
            Books =
            [
                new Book
                {
                    Id = Guid.NewGuid(),
                    Title = "Old Book",
                    Isbn = "1234567890123",
                    PublishedAt = new DateOnly(2020, 1, 1),
                    ArrivedAt = DateTimeOffset.UtcNow
                }
            ]
        };

        _context.Publishers.Add(publisher);
        await _context.SaveChangesAsync();

        var existingBookId = publisher.Books.First().Id;

        // Incoming dataset: One updated, one new, one missing (should be removed)
        var newBooks = new List<Book>
        {
            // Updating existing book
            new Book
            {
                Id = existingBookId,
                Title = "Updated Book",
                Isbn = "1234567890123",
                PublishedAt = new DateOnly(2021, 1, 1),
                ArrivedAt = DateTimeOffset.UtcNow
            },
            // Adding a new book
            new Book
            {
                //Id = Guid.NewGuid(),
                Title = "New Book",
                Isbn = "9876543210987",
                PublishedAt = new DateOnly(2022, 5, 15),
                ArrivedAt = DateTimeOffset.UtcNow
            },
        };

        // Act: Apply MergeByKey to sync Books with the new dataset
        publisher.Books.MergeByKey(
            data: newBooks,
            leftKey: book => book.Id,
            rightKey: bookDto => bookDto.Id,
            update: (existingBook, bookDto) =>
            {
                existingBook.Title = bookDto.Title;
                existingBook.Isbn = bookDto.Isbn;
                existingBook.PublishedAt = bookDto.PublishedAt;
                existingBook.ArrivedAt = bookDto.ArrivedAt;
            },
            create: bookDto => new Book
            {
                //Id = bookDto.Id,
                Title = bookDto.Title,
                Isbn = bookDto.Isbn,
                PublishedAt = bookDto.PublishedAt,
                ArrivedAt = bookDto.ArrivedAt,
            }
        );

        await _context.SaveChangesAsync();

        // Assert: Verify changes in the database
        var updatedPublisher = await _context.Publishers
            .Include(p => p.Books)
            .FirstAsync(p => p.Id == publisher.Id);

        var updatedBooks = updatedPublisher.Books;

        // There should be exactly 2 books (1 updated, 1 added)
        Assert.Equal(2, updatedBooks.Count);

        // Ensure the existing book was updated
        var updatedBook = updatedBooks.First(b => b.Id == existingBookId);
        Assert.Equal("Updated Book", updatedBook.Title);
        Assert.Equal(new DateOnly(2021, 1, 1), updatedBook.PublishedAt);

        // Ensure the new book was added
        var newBook = updatedBooks.First(b => b.Title == "New Book");
        Assert.Equal("9876543210987", newBook.Isbn);

        // Ensure the book that was not in `newBooks` was removed
        Assert.DoesNotContain(updatedBooks, b => b.Title == "Old Book");
    }
}