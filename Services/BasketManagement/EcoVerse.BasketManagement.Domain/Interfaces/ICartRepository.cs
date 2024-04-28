using EcoVerse.BasketManagement.Domain.Entities;

namespace EcoVerse.BasketManagement.Domain.Interfaces;

public interface ICartRepository
{
   Task<Cart> CreateAndGetCartAsync(string userId);
   
   Task<bool> AddItemAsync(string userId, Cart cart, CartItem cartItem);

   Task<Cart?> GetByUserId(string userId);

   Task UpdateQuantityAsync(string userId, Cart cart, CartItem cartItem, int quantity);

   Task DeleteItemAsync(string userId, Cart cart, CartItem cartItem);

   Task CheckoutAsync();
}