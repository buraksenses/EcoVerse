using EcoVerse.BasketManagement.Application.DTOs;
using EcoVerse.BasketManagement.Application.Interfaces;
using EcoVerse.BasketManagement.Application.Mappings;
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
        var cartDto = await GetByUserId(userId);

        var cart = ObjectMapper.Mapper.Map<Cart>(cartDto.Data);

        cart.LastModifiedBy = Guid.Parse(userId);
        cart.LastModifiedDate = DateTime.Now;

        await _cartRepository.AddItemAsync(userId, cart, new CartItem
        {
            Id = Guid.NewGuid(),
            Price = addToCartDto.Price,
            ProductId = addToCartDto.ProductId,
            Quantity = addToCartDto.Quantity
        });

        return Response<bool>.Success(201);
    }

    public async Task<Response<GetCartDto>> GetByUserId(string userId)
    {
        var cart = await _cartRepository.GetByUserId(userId) 
                   ?? await _cartRepository.CreateAndGetCartAsync(userId);

        return Response<GetCartDto>.Success(ObjectMapper.Mapper.Map<GetCartDto>(cart), 200);
    }

    public async Task<Response<NoContent>> UpdateQuantityAsync(string userId, Guid itemId, UpdateCartDto updateCartDto)
    {
        var cartDto = await GetByUserId(userId);

        var cart = ObjectMapper.Mapper.Map<Cart>(cartDto.Data);

        var item = cart.CartItems.FirstOrDefault(c => c.Id == itemId);

        IsCartItemValid(item);

        cart.LastModifiedBy = Guid.Parse(userId);
        cart.LastModifiedDate = DateTime.Now;
        
        await _cartRepository.UpdateQuantityAsync(userId, cart, item, updateCartDto.Quantity);
        
        return Response<NoContent>.Success(200);
    }

    public async Task<Response<NoContent>> DeleteItemAsync(string userId, DeleteFromCartDto deleteFromCartDto)
    {
        var cartDto = await GetByUserId(userId);

        var cart = ObjectMapper.Mapper.Map<Cart>(cartDto);
        
        var item = cart.CartItems.FirstOrDefault(c => c.Id == deleteFromCartDto.ItemId);

        IsCartItemValid(item);
        
        cart.LastModifiedBy = Guid.Parse(userId);
        cart.LastModifiedDate = DateTime.Now;
        
        await _cartRepository.DeleteItemAsync(userId, cart, item);
        
        return Response<NoContent>.Success(200);
    }

    public Task CheckoutAsync()
    {
        return null;
    }

    private static void IsCartItemValid(CartItem? cartItem)
    {
        if (cartItem == null)
            throw new CartItemNotFoundException("Could not found an item with given ID!");
    }
}