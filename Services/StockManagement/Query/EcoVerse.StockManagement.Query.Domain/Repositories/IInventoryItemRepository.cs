using EcoVerse.StockManagement.Query.Domain.Entities;

namespace EcoVerse.StockManagement.Query.Domain.Repositories;

public interface IInventoryItemRepository
{
    Task AddAsync(InventoryItemEntity entity);

    Task UpdateAsync(InventoryItemEntity entity);

    Task RemoveAsync(InventoryItemEntity entity);

    Task<InventoryItemEntity?> GetByIdAsync(Guid id);

    Task<List<InventoryItemEntity>> GetAllAsync();

    Task<List<InventoryItemEntity>> GetAllByQuantity(int quantity);
    
    Task<List<InventoryItemEntity>> GetAllByPrice(decimal price);
}