using EcoVerse.ProductManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EcoVerse.ProductManagement.Infrastructure.Data.Context;

public class ProductDbContext : DbContext
{
    public ProductDbContext(DbContextOptions<ProductDbContext> options) : base(options)
    {
        
    }

    public DbSet<Product> Products { get; set; }

    public DbSet<Category> Categories { get; set; }
}