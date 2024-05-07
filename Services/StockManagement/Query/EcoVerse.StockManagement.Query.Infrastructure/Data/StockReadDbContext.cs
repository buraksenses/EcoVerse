using EcoVerse.StockManagement.Query.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EcoVerse.StockManagement.Query.Infrastructure.Data;

public class StockReadDbContext : DbContext
{
    public StockReadDbContext(DbContextOptions<StockReadDbContext> options) : base(options)
    {
        
    }

    public DbSet<InventoryItemEntity> InventoryItems { get; set; }
}