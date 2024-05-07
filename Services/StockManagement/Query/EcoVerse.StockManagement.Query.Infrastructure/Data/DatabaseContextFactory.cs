using Microsoft.EntityFrameworkCore;

namespace EcoVerse.StockManagement.Query.Infrastructure.Data;

public class DatabaseContextFactory
{
    private readonly Action<DbContextOptionsBuilder> _configureDbContext;

    public DatabaseContextFactory(Action<DbContextOptionsBuilder> configureDbContext)
    {
        _configureDbContext = configureDbContext;
    }

    public StockReadDbContext CreateDbContext()
    {
        DbContextOptionsBuilder<StockReadDbContext> optionsBuilder = new();

        _configureDbContext(optionsBuilder);

        return new StockReadDbContext(optionsBuilder.Options);
    }
}