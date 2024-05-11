using CQRS.Core.Handlers;
using EcoVerse.Shared.Messages;
using EcoVerse.StockManagement.Command.Domain.Aggregates;
using MassTransit;

namespace EcoVerse.StockManagement.Command.Application.Consumers;

public class StockCheckResponseEventConsumer : IConsumer<StockCheckResponseEvent>
{
    private readonly IEventSourcingHandler<InventoryItemAggregate> _eventSourcingHandler;

    public StockCheckResponseEventConsumer(IEventSourcingHandler<InventoryItemAggregate> eventSourcingHandler)
    {
        _eventSourcingHandler = eventSourcingHandler;
    }
    
    public async Task Consume(ConsumeContext<StockCheckResponseEvent> context)
    {
        var message = context.Message;

        var aggregate = await _eventSourcingHandler.GetByInventoryItemIdAsync(message.ProductId);
        aggregate.UpdateInventoryItemQuantity(message.StockQuantity);
        await _eventSourcingHandler.SaveAsync(aggregate);
    }
}