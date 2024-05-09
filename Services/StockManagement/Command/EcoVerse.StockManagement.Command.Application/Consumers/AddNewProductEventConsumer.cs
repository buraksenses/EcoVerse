using CQRS.Core.Handlers;
using EcoVerse.Shared.Messages;
using EcoVerse.StockManagement.Command.Domain.Aggregates;
using MassTransit;

namespace EcoVerse.StockManagement.Command.Application.Consumers;

public class AddNewProductEventConsumer : IConsumer<AddNewProductEvent>
{
    private readonly IEventSourcingHandler<InventoryItemAggregate> _eventSourcingHandler;

    public AddNewProductEventConsumer(IEventSourcingHandler<InventoryItemAggregate> eventSourcingHandler)
    {
        _eventSourcingHandler = eventSourcingHandler;
    }
    
    public async Task Consume(ConsumeContext<AddNewProductEvent> context)
    {
        var message = context.Message;

        var aggregate = new InventoryItemAggregate(Guid.NewGuid(), message.ProductId, message.Name, message.Price,
            message.Description, message.Quantity);
        await _eventSourcingHandler.SaveAsync(aggregate);
    }
}