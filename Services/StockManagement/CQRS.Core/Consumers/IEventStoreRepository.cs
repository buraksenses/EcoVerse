using CQRS.Core.Events;

namespace CQRS.Core.Consumers;

public interface IEventStoreRepository
{
    Task SaveAsync(EventModel @event);
    Task<List<EventModel>> FindByAggregateId(Guid aggregateId);
    Task<List<EventModel>> FindAllAsync();
}