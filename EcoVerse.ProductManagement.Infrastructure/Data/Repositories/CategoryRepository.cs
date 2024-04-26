using EcoVerse.ProductManagement.Domain.Entities;
using EcoVerse.ProductManagement.Domain.Interfaces;
using EcoVerse.ProductManagement.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace EcoVerse.ProductManagement.Infrastructure.Data.Repositories;

public class CategoryRepository : ICategoryRepository
{
    private readonly ProductDbContext _dbContext;

    public CategoryRepository(ProductDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task CreateAsync(Category category)
    {
        await _dbContext.Categories.AddAsync(category);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<List<Category>> ListAllAsync(string? filterOn = null, string? filterQuery = null,string?
            sortBy = null,bool? isAscending = null,
        int pageNumber = 1, int pageSize = 1000)
    {
        var categories = _dbContext.Categories.AsQueryable();

        categories = FilterResults(categories, filterOn, filterQuery);

        categories =  SortResults(categories,sortBy,isAscending);
        
        //Pagination
        var skipResults = (pageNumber - 1) * pageSize;

        return await categories.Skip(skipResults).Take(pageSize).ToListAsync();
    }

    public async Task UpdateAsync(Category category)
    {
        _dbContext.Categories.Update(category);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(Category category)
    {
        _dbContext.Categories.Remove(category);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<Category?> GetByIdAsync(Guid id)
    {
        var category = await _dbContext.Categories.FirstOrDefaultAsync(c => c.Id == id);
        return category;
    }
    
    private static IQueryable<Category> FilterResults(IQueryable<Category> categories, string? filterOn, string? filterQuery)
    {
        if (string.IsNullOrWhiteSpace(filterOn) || string.IsNullOrWhiteSpace(filterQuery)) 
            return categories;
        
        if (filterOn.Equals("Name", StringComparison.OrdinalIgnoreCase))
            categories = categories.Where(x => x.Name.Contains(filterQuery));
            
        else if (filterOn.Equals("Description", StringComparison.OrdinalIgnoreCase))
            categories = categories.Where(x => x.Description.Contains(filterQuery));

        return categories;
    }

    private static IQueryable<Category> SortResults(IQueryable<Category> categories, string? sortBy, bool? isAscending)
    {
        if (string.IsNullOrWhiteSpace(sortBy) || isAscending == null) 
            return categories;
        
        if (sortBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
            categories = isAscending.Value ? categories.OrderBy(x => x.Name) : categories.OrderByDescending(x => x.Name);
        else if (sortBy.Equals("Description", StringComparison.OrdinalIgnoreCase))
            categories = isAscending.Value
                ? categories.OrderBy(x => x.Description)
                : categories.OrderByDescending(x => x.Description);

        return categories;
    }
}