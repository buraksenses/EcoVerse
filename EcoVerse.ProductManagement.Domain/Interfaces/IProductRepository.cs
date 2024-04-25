using EcoVerse.ProductManagement.Domain.Entities;

namespace EcoVerse.ProductManagement.Domain.Interfaces;

public interface IProductRepository
{
    Task CreateAsync(Product product);

    Task<List<Product>> ListAllAsync();

    Task UpdateAsync(Guid id, Product product);

    Task DeleteAsync(Guid id);

    Task<Product> GetByIdAsync(Guid id);
}