using Microsoft.EntityFrameworkCore;
using Oibi.Repository.Demo;
using Oibi.Repository.Demo.Repositories;
using Oibi.TestHelper;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Oibi.Repository.Tests;

public class DatabaseConnectionTest : IClassFixture<ServerFixture<Startup>>
{
    private readonly ServerFixture<Startup> _serverFixture;
    private readonly LibraryDbScope _libraryScope;

    public DatabaseConnectionTest(ServerFixture<Startup> serverFixture)
    {
        _serverFixture = serverFixture;
        _libraryScope = _serverFixture.GetService<LibraryDbScope>();
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
}