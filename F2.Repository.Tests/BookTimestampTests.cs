using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using F2.Repository.Demo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
