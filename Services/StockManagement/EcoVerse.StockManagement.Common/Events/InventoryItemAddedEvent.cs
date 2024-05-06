using CQRS.Core.Events;

namespace EcoVerse.StockManagement.Common.Events;

public class InventoryItemAddedEvent : BaseEvent
{
    protected InventoryItemAddedEvent(string type) : base(type)
    {
    }
}