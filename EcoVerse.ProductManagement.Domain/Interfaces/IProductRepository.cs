﻿using EcoVerse.ProductManagement.Domain.Entities;
using EcoVerse.Shared.DTOs;

namespace EcoVerse.ProductManagement.Domain.Interfaces;

public interface IProductRepository
{
    Task CreateAsync(Product product);

    Task<List<Product>> ListAllAsync();

    Task UpdateAsync(Product product);

    Task DeleteAsync(Product product);

    Task<Product?> GetByIdAsync(Guid id);

    Task<List<Product>> GetByCategory(Guid categoryId);
}