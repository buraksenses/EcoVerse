﻿using EcoVerse.StockManagement.Query.Domain.Entities;
using EcoVerse.StockManagement.Query.Domain.Repositories;
using EcoVerse.StockManagement.Query.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace EcoVerse.StockManagement.Query.Infrastructure.Repositories;

public class InventoryItemRepository : IInventoryItemRepository
{
    private readonly DatabaseContextFactory _contextFactory;

    public InventoryItemRepository(DatabaseContextFactory contextFactory)
    {
        _contextFactory = contextFactory;
    }
    
    public async Task AddAsync(InventoryItemEntity entity)
    {
        await using var context = _contextFactory.CreateDbContext();
        await context.InventoryItems.AddAsync(entity);
        _ = await context.SaveChangesAsync();
    }

    public async Task UpdateAsync(InventoryItemEntity entity)
    {
        await using var context = _contextFactory.CreateDbContext();
        context.InventoryItems.Update(entity);
        _ = await context.SaveChangesAsync();
    }

    public async Task RemoveAsync(InventoryItemEntity entity)
    {
        await using var context = _contextFactory.CreateDbContext();

        context.InventoryItems.Remove(entity);

        _ = await context.SaveChangesAsync();
    }

    public async Task<InventoryItemEntity> GetByIdAsync(Guid id)
    {
        await using var context = _contextFactory.CreateDbContext();

        return await context.InventoryItems.FirstOrDefaultAsync(i => i.Id == id);
    }

    public async Task<InventoryItemEntity?> GetByProductIdAsync(Guid productId)
    {
        await using var context = _contextFactory.CreateDbContext();

        return await context.InventoryItems.FirstOrDefaultAsync(i => i.ProductId == productId);
    }

    public async Task<List<InventoryItemEntity>> GetAllAsync()
    {
        await using var context = _contextFactory.CreateDbContext();

        return await context.InventoryItems.AsNoTracking().ToListAsync();
    }

    public async Task<List<InventoryItemEntity>> GetAllByQuantity(int quantity)
    {
        await using var context = _contextFactory.CreateDbContext();

        return await context.InventoryItems.Where(i => i.Quantity == quantity).ToListAsync();
    }

    public async Task<List<InventoryItemEntity>> GetAllByPrice(decimal price)
    {
        await using var context = _contextFactory.CreateDbContext();

        return await context.InventoryItems.Where(i => i.Price == price).ToListAsync();
    }
}