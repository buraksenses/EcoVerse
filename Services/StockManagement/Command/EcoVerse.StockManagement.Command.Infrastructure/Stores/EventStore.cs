using CQRS.Core.Consumers;
using CQRS.Core.Events;
using CQRS.Core.Infrastructure;
using CQRS.Core.Producers;
using EcoVerse.Shared.Exceptions;
using EcoVerse.StockManagement.Command.Domain.Aggregates;

namespace EcoVerse.StockManagement.Command.Infrastructure.Stores;

public class EventStore : IEventStore
{
    private readonly IEventStoreRepository _repository;
    private readonly IEventProducer _eventProducer;

    public EventStore(IEventStoreRepository repository, IEventProducer eventProducer)
    {
        _repository = repository;
        _eventProducer = eventProducer;
    }

    public async Task SaveEventsAsync(Guid aggregateId, IEnumerable<BaseEvent> events, int expectedVersion)
    {
        var eventStream = await _repository.FindByAggregateId(aggregateId);

        if (expectedVersion != -1 && eventStream[^1].Version != expectedVersion)
            throw new ConcurrencyException();

        var version = expectedVersion;

        foreach (var @event in events)
        {
            version++;
            @event.Version = version;
            var eventType = @event.GetType().Name;
            var eventModel = new EventModel
            {
                TimeStamp = DateTime.Now,
                AggregateIdentifier = aggregateId,
                AggregateType = nameof(InventoryItemAggregate),
                Version = version,
                EventType = eventType,
                EventData = @event
            };

            await _repository.SaveAsync(eventModel);

            var topic = Environment.GetEnvironmentVariable("KAFKA_TOPIC");

            await _eventProducer.ProduceAsync(topic, @event);
        }
    }

    public async Task<List<BaseEvent>> GetEventsAsync(Guid aggregateId)
    {
        var eventStream = await _repository.FindByAggregateId(aggregateId);

        if (eventStream == null || !eventStream.Any())
                throw new AggregateNotFoundException("Incorrect ID provided!");

        return eventStream.OrderBy(x => x.Version).Select(x => x.EventData).ToList();
    }
    
    public async Task<List<BaseEvent>> GetEventsByItemIdAsync(Guid inventoryItemId)
    {
        var eventStream = await _repository.FindByInventoryItemId(inventoryItemId);

        if (eventStream == null || !eventStream.Any())
            throw new AggregateNotFoundException("Incorrect item ID provided!");

        return eventStream.OrderBy(x => x.Version).Select(x => x.EventData).ToList();
    }
}