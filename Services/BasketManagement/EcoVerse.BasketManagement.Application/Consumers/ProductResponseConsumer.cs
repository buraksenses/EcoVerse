using EcoVerse.BasketManagement.Application.DTOs;
using EcoVerse.BasketManagement.Application.Interfaces;
using EcoVerse.Shared.Messages;
using MassTransit;

namespace EcoVerse.BasketManagement.Application.Consumers;

public class ProductResponseConsumer : IConsumer<ProductCheckResponseEvent>
{
    private readonly ICartService _cartService;

    public ProductResponseConsumer(ICartService cartService)
    {
        _cartService = cartService;
    }
    
    public async Task Consume(ConsumeContext<ProductCheckResponseEvent> context)
    {
        var message = context.Message;

        if (message.Exists)
        {
            await _cartService.AddItemAsync(message.UserId,
                new AddToCartDto(message.ProductId, message.Quantity, message.Price));
        }
    }
}