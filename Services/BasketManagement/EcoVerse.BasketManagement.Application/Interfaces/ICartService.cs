using EcoVerse.BasketManagement.Application.DTOs;
using EcoVerse.BasketManagement.Domain.Entities;
using EcoVerse.Shared.DTOs;

namespace EcoVerse.BasketManagement.Application.Interfaces;

public interface ICartService
{
    Task<Response<bool>> AddItemAsync(string userId, AddToCartDto addToCartDto);

    Task<Response<Cart>> GetByUserId(string userId);

    Task<Response<NoContent>> UpdateQuantityAsync(string userId, Guid itemId, UpdateCartDto updateCartDto);

    Task<Response<NoContent>> DeleteItemAsync(string userId, DeleteFromCartDto deleteFromCartDto);

    Task CheckoutAsync();
}