namespace EcoVerse.StockManagement.Command.Application.DTOs;

public record AddInventoryItemDto(Guid ProductId, string Name, string Description, decimal Price, int Quantity);