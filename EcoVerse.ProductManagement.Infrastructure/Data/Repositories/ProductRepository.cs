using EcoVerse.ProductManagement.Domain.Entities;
using EcoVerse.ProductManagement.Domain.Interfaces;
using EcoVerse.ProductManagement.Infrastructure.Data.Context;

namespace EcoVerse.ProductManagement.Infrastructure.Data.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly ProductDbContext _dbContext;

    public ProductRepository(ProductDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task CreateAsync(Product product)
    {
        await _dbContext.AddAsync(product);
        await _dbContext.SaveChangesAsync();
    }

    public Task<List<Product>> ListAllAsync()
    {
        return null;
    }

    public Task UpdateAsync(Guid id, Product product)
    {
        return null;
    }

    public Task DeleteAsync(Guid id)
    {
        return null;
    }

    public Task<Product> GetByIdAsync(Guid id)
    {
        return null;
    }
}