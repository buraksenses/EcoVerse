namespace EcoVerse.StockManagement.Query.Application.DTOs;

public record InventoryItemDto(Guid ProductId, string Name, string Description, decimal Price , int Quantity);