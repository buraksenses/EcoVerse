namespace EcoVerse.BasketManagement.Application.DTOs;

public record AddToCartDto(Guid ProductId, int Quantity, decimal Price, string Name, string Description);