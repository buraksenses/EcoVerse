using EcoVerse.BasketManagement.Domain.Entities;

namespace EcoVerse.BasketManagement.Application.DTOs;

public record GetCartDto(
    decimal TotalAmount,
    string UserId,
    List<CartItem> CartItems,
    decimal TotalPrice
    );