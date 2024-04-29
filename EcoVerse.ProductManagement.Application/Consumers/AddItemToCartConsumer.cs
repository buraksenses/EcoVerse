using EcoVerse.ProductManagement.Infrastructure.Data.Context;
using EcoVerse.Shared.Messages;
using MassTransit;
using Microsoft.EntityFrameworkCore;

namespace EcoVerse.ProductManagement.Application.Consumers;

public class AddItemToCartConsumer : IConsumer<AddItemToCartCommand>
{
    private readonly ProductDbContext _dbContext;
    private readonly IPublishEndpoint _publishEndpoint;

    public AddItemToCartConsumer(ProductDbContext dbContext, IPublishEndpoint publishEndpoint)
    {
        _dbContext = dbContext;
        _publishEndpoint = publishEndpoint;
    }
    
    public async Task Consume(ConsumeContext<AddItemToCartCommand> context)
    {
        var message = context.Message;

        var product = await _dbContext.Products.FirstOrDefaultAsync(p => p.Id == message.ProductId);

        if (product != null)
        {
            await _publishEndpoint.Publish<ProductCheckResponseEvent>(new ProductCheckResponseEvent
            {
               ProductId = message.ProductId,
               Exists = true,
               Price = message.Price,
               UserId = message.UserId,
               Quantity = message.Quantity
            });
        }
    }
}