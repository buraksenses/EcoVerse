using CQRS.Core.Events;

namespace EcoVerse.StockManagement.Common.Events;

public class InventoryItemUpdatedEvent : BaseEvent
{
    protected InventoryItemUpdatedEvent(string type) : base(type)
    {
    }
}