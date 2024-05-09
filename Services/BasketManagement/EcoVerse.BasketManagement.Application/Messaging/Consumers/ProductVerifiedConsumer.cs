using EcoVerse.BasketManagement.Application.DTOs;
using EcoVerse.BasketManagement.Application.Interfaces;
using EcoVerse.Shared.Messages;
using MassTransit;

namespace EcoVerse.BasketManagement.Application.Messaging.Consumers;

public class ProductVerifiedConsumer : IConsumer<StockCheckResponseEvent>
{
    private readonly ICartService _cartService;

    public ProductVerifiedConsumer(ICartService cartService)
    {
        _cartService = cartService;
    }
    public async Task Consume(ConsumeContext<StockCheckResponseEvent> context)
    {
        await _cartService.AddItemAsync(context.Message.UserId,
            new AddToCartDto(context.Message.ProductId, context.Message.Quantity, context.Message.Price));
    }
}