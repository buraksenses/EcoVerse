using System.Text.Json;
using EcoVerse.BasketManagement.Domain.Entities;
using EcoVerse.BasketManagement.Domain.Interfaces;
using EcoVerse.BasketManagement.Infrastructure.Services;
using EcoVerse.Shared.DTOs;
using EcoVerse.Shared.Exceptions;

namespace EcoVerse.BasketManagement.Infrastructure.Repositories;

public class CartRepository : ICartRepository
{
    private readonly RedisService _redisService;

    public CartRepository(RedisService redisService)
    {
        _redisService = redisService;
    }

    public async Task<Cart> CreateAndGetCartAsync(string userId)
    { 
        await _redisService.GetDb().StringSetAsync(userId, JsonSerializer.Serialize(new Cart{UserId = userId, CreatedBy = Guid.Parse(userId)}));
        
        var existBasket = await _redisService.GetDb().StringGetAsync(userId);

        return JsonSerializer.Deserialize<Cart>(existBasket);
    }

    public async Task<bool> AddItemAsync(string userId, Cart cart, CartItem cartItem)
    {
        if (cart.CartItems.Exists(x => x.Id == cartItem.Id))
            throw new CartItemConcurrencyException("Can not add an item that has the same id with one of the list items!");
        
        cart.CartItems.Add(cartItem);

        return await SaveAsync(userId, cart);
    }

    public async Task<Cart?> GetByUserId(string userId)
    {
        var existBasket = await _redisService.GetDb().StringGetAsync(userId);
        
        return existBasket.HasValue ? JsonSerializer.Deserialize<Cart>(existBasket) : null;
    }

    public async Task UpdateQuantityAsync(string userId, Cart cart, CartItem cartItem, int quantity)
    {
        cartItem.Quantity = quantity;

        await SaveAsync(userId, cart);
    }

    public async Task DeleteItemAsync(string userId, Cart cart, CartItem cartItem)
    {
        cart.CartItems.Remove(cartItem);

        await SaveAsync(userId, cart);
    }

    private async Task<bool> SaveAsync(string userId, Cart cart)
    {
        var status = await _redisService.GetDb().StringSetAsync(userId, JsonSerializer.Serialize(cart));

        if (!status)
            throw new CartSaveException("Cart could not be saved!");

        return status;
    }

    public Task CheckoutAsync()
    {
        return null;
    }
}