using EcoVerse.ProductManagement.Domain.Entities;
using EcoVerse.ProductManagement.Domain.Interfaces;
using EcoVerse.ProductManagement.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace EcoVerse.ProductManagement.Infrastructure.Data.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly ProductDbContext _dbContext;

    public ProductRepository(ProductDbContext dbContext) => _dbContext = dbContext;

    public async Task CreateAsync(Product product)
    {
        await _dbContext.Products.AddAsync(product);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<List<Product>> ListAllAsync(string? filterOn = null, string? filterQuery = null,string?
            sortBy = null,bool? isAscending = null,
        int pageNumber = 1, int pageSize = 1000)
    {
        var products = _dbContext.Products.AsQueryable();

        products = FilterResults(products, filterOn, filterQuery);

        products =  SortResults(products,sortBy,isAscending);
        
        //Pagination
        var skipResults = (pageNumber - 1) * pageSize;

        return await products.Skip(skipResults).Take(pageSize).ToListAsync();
    }

    public async Task UpdateAsync(Product product)
    {
        _dbContext.Products.Update(product);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(Product product)
    {
        _dbContext.Products.Remove(product);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<Product?> GetByIdAsync(Guid id) 
        => await _dbContext.Products.FirstOrDefaultAsync(p => p.Id == id);
    
    public async Task<List<Product>> GetByCategory(Guid categoryId) 
        => await _dbContext.Products.Where(p => p.CategoryId == categoryId).ToListAsync();


    private static IQueryable<Product> FilterResults(IQueryable<Product> products, string? filterOn, string? filterQuery)
    {
        if (string.IsNullOrWhiteSpace(filterOn) || string.IsNullOrWhiteSpace(filterQuery)) 
            return products;
        
        if (filterOn.Equals("Name", StringComparison.OrdinalIgnoreCase))
            products = products.Where(x => x.Name.Contains(filterQuery));
            
        else if (filterOn.Equals("Description", StringComparison.OrdinalIgnoreCase))
            products = products.Where(x => x.Description.Contains(filterQuery));

        else if (filterOn.Equals("Quantity", StringComparison.OrdinalIgnoreCase))
            products = products.Where(x => Math.Abs(x.Quantity - float.Parse(filterQuery)) < 1);
            
        else if (filterOn.Equals("Price", StringComparison.OrdinalIgnoreCase))
            products = products.Where(x => Math.Abs(x.Price - decimal.Parse(filterQuery)) < 1);

        return products;
    }

    private static IQueryable<Product> SortResults(IQueryable<Product> products, string? sortBy, bool? isAscending)
    {
        if (string.IsNullOrWhiteSpace(sortBy) || isAscending == null) 
            return products;
        
        if (sortBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
            products = isAscending.Value ? products.OrderBy(x => x.Name) : products.OrderByDescending(x => x.Name);
        else if (sortBy.Equals("Description", StringComparison.OrdinalIgnoreCase))
            products = isAscending.Value
                ? products.OrderBy(x => x.Description)
                : products.OrderByDescending(x => x.Description);
        else if (sortBy.Equals("Quantity", StringComparison.OrdinalIgnoreCase))
            products = isAscending.Value
                ? products.OrderBy(x => x.Quantity)
                : products.OrderByDescending(x => x.Quantity);
        else if (sortBy.Equals("Price", StringComparison.OrdinalIgnoreCase))
            products = isAscending.Value
                ? products.OrderBy(x => x.Price)
                : products.OrderByDescending(x => x.Price);

        return products;
    }

}
//category olustur. product olustur. sonra sagayi test et.