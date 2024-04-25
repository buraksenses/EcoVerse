﻿using EcoVerse.ProductManagement.Domain.Entities;
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

    public async Task<List<Product>> ListAllAsync() 
        => await _dbContext.Products.AsNoTracking().ToListAsync();
    
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
    
}