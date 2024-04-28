namespace EcoVerse.BasketManagement.Application.DTOs;

public record UpdateCartDto(
    Guid ItemId,
    int quantity
    );