using EcoVerse.ProductManagement.Domain.Entities;

namespace EcoVerse.ProductManagement.Domain.Interfaces;

public interface ICategoryRepository
{
    Task CreateAsync(Category category);

    Task<List<Category>> ListAllAsync(string? filterOn = null, string? filterQuery = null,string?
            sortBy = null,bool? isAscending = null,
        int pageNumber = 1, int pageSize = 1000);

    Task UpdateAsync(Category category);

    Task DeleteAsync(Category category);

    Task<Category?> GetByIdAsync(Guid id);
}