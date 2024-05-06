using CQRS.Core.Domain;
using CQRS.Core.Handlers;
using CQRS.Core.Infrastructure;
using EcoVerse.StockManagement.Command.Domain.Aggregates;

namespace EcoVerse.StockManagement.Command.Infrastructure.Handlers;

public class EventSourcingHandler : IEventSourcingHandler<InventoryItemAggregate>
{
    private readonly IEventStore _eventStore;

    public EventSourcingHandler(IEventStore eventStore)
    {
        _eventStore = eventStore;
    }
    
    public async Task SaveAsync(AggregateRoot aggregate)
    {
        await _eventStore.SaveEventsAsync(aggregate.Id, aggregate.GetUncommittedChanges(), aggregate.Version);
        aggregate.MarkChangesAsCommitted();
    }

    public async Task<InventoryItemAggregate> GetByIdAsync(Guid aggregateId)
    {
        var aggregate = new InventoryItemAggregate();

        var events = await _eventStore.GetEventsAsync(aggregateId);

        if (events == null || !events.Any())
            return aggregate;
        
        aggregate.ReplayEvents(events);
        aggregate.Version = events.Select(x => x.Version).Max();

        return aggregate;
    }
}