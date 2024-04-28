using EcoVerse.BasketManagement.Domain.Entities;
using EcoVerse.Shared.DTOs;

namespace EcoVerse.BasketManagement.Infrastructure.Interfaces;

public interface ICartService
{
    Task<Response<bool>> AddItemAsync(string userId, CartItem cartItem);

    Task<Response<Cart>> GetByUserId(string userId);

    Task<Response<NoContent>> UpdateQuantityAsync(string userId, Guid itemId, int quantity);

    Task<Response<NoContent>> DeleteItemAsync(string userId, Guid itemId);

    Task CheckoutAsync();
}