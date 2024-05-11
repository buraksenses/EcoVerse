using EcoVerse.Shared.Messages;
using EcoVerse.StockManagement.Query.Domain.Repositories;
using MassTransit;

namespace EcoVerse.StockManagement.Query.Application.Consumers;

public class StockVerificationConsumer : IConsumer<AddItemToCartEvent>
{
    private readonly IInventoryItemRepository repository;
    private readonly IPublishEndpoint _publishEndpoint;

    public StockVerificationConsumer(IPublishEndpoint publishEndpoint, IInventoryItemRepository repository)
    {
        _publishEndpoint = publishEndpoint;
        this.repository = repository;
    }
    
    public async Task Consume(ConsumeContext<AddItemToCartEvent> context)
    {
        var message = context.Message;

        var product = await repository.GetByProductIdAsync(message.ProductId);
        if (product != null && product.Quantity > message.Quantity && product.ProductId == message.ProductId)
        {
            await _publishEndpoint.Publish<StockCheckResponseEvent>(new StockCheckResponseEvent
            {
                ProductId = message.ProductId,
                IsInStock = true,
                Name = message.Name,
                Description = message.Description,
                Price = message.Price,
                StockQuantity = product.Quantity - message.Quantity,
                CartQuantity = message.Quantity,
                UserId = message.UserId
            });
        }
    }
}