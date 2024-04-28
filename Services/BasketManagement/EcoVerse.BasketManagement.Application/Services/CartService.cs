using EcoVerse.BasketManagement.Domain.Entities;
using EcoVerse.BasketManagement.Domain.Interfaces;
using EcoVerse.BasketManagement.Infrastructure.Interfaces;
using EcoVerse.Shared.DTOs;

namespace EcoVerse.BasketManagement.Application.Services;

public class CartService : ICartService
{
    private readonly ICartRepository _cartRepository;

    public CartService(ICartRepository cartRepository)
    {
        _cartRepository = cartRepository;
    }
    
    public async Task<Response<bool>> AddItemAsync(string userId, CartItem cartItem)
    {
        var cart = await GetByUserId(userId);

        await _cartRepository.AddItemAsync(userId, cart.Data, cartItem);
        
        return Response<bool>.Success(201);
    }

    public async Task<Response<Cart>> GetByUserId(string userId)
    {
        var cart = await _cartRepository.GetByUserId(userId) 
                   ?? await _cartRepository.CreateAndGetCartAsync(userId);

        return Response<Cart>.Success(cart, 200);
    }

    public async Task<Response<NoContent>> UpdateQuantityAsync(string userId, Guid itemId, int quantity)
    {
        var cart = await GetByUserId(userId);

        var item = cart.Data.CartItems.FirstOrDefault(c => c.Id == itemId);

        if (item == null)
            throw new Exception("Item cannot be null!");

        await _cartRepository.UpdateQuantityAsync(userId, cart.Data, item, quantity);
        
        return Response<NoContent>.Success(204);
    }

    public async Task<Response<NoContent>> DeleteItemAsync(string userId, Guid itemId)
    {
        var cart = await GetByUserId(userId);
        var item = cart.Data.CartItems.FirstOrDefault(c => c.Id == itemId);

        if (item == null)
            throw new Exception("Item cannot be null!");
        
        await _cartRepository.DeleteItemAsync(userId, cart.Data, item);
        
        return Response<NoContent>.Success(204);
    }

    public Task CheckoutAsync()
    {
        return null;
    }
}