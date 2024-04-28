using EcoVerse.BasketManagement.Application.DTOs;
using EcoVerse.BasketManagement.Application.Interfaces;
using EcoVerse.BasketManagement.Domain.Entities;
using EcoVerse.BasketManagement.Domain.Interfaces;
using EcoVerse.Shared.DTOs;
using EcoVerse.Shared.Exceptions;

namespace EcoVerse.BasketManagement.Application.Services;

public class CartService : ICartService
{
    private readonly ICartRepository _cartRepository;

    public CartService(ICartRepository cartRepository)
    {
        _cartRepository = cartRepository;
    }
    
    public async Task<Response<bool>> AddItemAsync(string userId, AddToCartDto addToCartDto)
    {
        var cart = await GetByUserId(userId);

        await _cartRepository.AddItemAsync(userId, cart.Data, addToCartDto.CartItem);

        cart.Data.LastModifiedBy = Guid.Parse(userId);
        cart.Data.LastModifiedDate = DateTime.Now;

        return Response<bool>.Success(201);
    }

    public async Task<Response<Cart>> GetByUserId(string userId)
    {
        var cart = await _cartRepository.GetByUserId(userId) 
                   ?? await _cartRepository.CreateAndGetCartAsync(userId);

        return Response<Cart>.Success(cart, 200);
    }

    public async Task<Response<NoContent>> UpdateQuantityAsync(string userId, Guid itemId, UpdateCartDto updateCartDto)
    {
        var cart = await GetByUserId(userId);

        var item = cart.Data.CartItems.FirstOrDefault(c => c.Id == itemId);

        if (item == null)
            throw new CartItemNotFoundException("Could not found an item with given ID!");

        await _cartRepository.UpdateQuantityAsync(userId, cart.Data, item, updateCartDto.Quantity);
        
        cart.Data.LastModifiedBy = Guid.Parse(userId);
        cart.Data.LastModifiedDate = DateTime.Now;
        
        return Response<NoContent>.Success(204);
    }

    public async Task<Response<NoContent>> DeleteItemAsync(string userId, DeleteFromCartDto deleteFromCartDto)
    {
        var cart = await GetByUserId(userId);
        var item = cart.Data.CartItems.FirstOrDefault(c => c.Id == deleteFromCartDto.ItemId);

        if (item == null)
            throw new CartItemNotFoundException("Could not found an item with given ID!");
        
        await _cartRepository.DeleteItemAsync(userId, cart.Data, item);
        
        cart.Data.LastModifiedBy = Guid.Parse(userId);
        cart.Data.LastModifiedDate = DateTime.Now;
        
        return Response<NoContent>.Success(204);
    }

    public Task CheckoutAsync()
    {
        return null;
    }
}