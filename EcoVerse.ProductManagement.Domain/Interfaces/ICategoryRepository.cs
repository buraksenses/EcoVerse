using EcoVerse.ProductManagement.Domain.Entities;

namespace EcoVerse.ProductManagement.Domain.Interfaces;

public interface ICategoryRepository
{
    Task CreateAsync(Category category);

    Task<List<Category>> ListAllAsync();

    Task UpdateAsync(Category category);

    Task DeleteAsync(Category category);

    Task<Category?> GetByIdAsync(Guid id);
}