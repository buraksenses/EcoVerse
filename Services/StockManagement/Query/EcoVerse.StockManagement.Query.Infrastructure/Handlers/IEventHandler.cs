using EcoVerse.StockManagement.Common.Events;

namespace EcoVerse.StockManagement.Query.Infrastructure.Handlers;

public interface IEventHandler
{
    Task On(InventoryItemAddedEvent @event);
    Task On(InventoryItemPriceUpdatedEvent @event);
    Task On(InventoryItemQuantityUpdatedEvent @event);
    Task On(InventoryItemRemovedEvent @event);
}