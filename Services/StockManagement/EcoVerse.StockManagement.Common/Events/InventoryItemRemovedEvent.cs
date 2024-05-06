using CQRS.Core.Events;

namespace EcoVerse.StockManagement.Common.Events;

public class InventoryItemRemovedEvent : BaseEvent
{
    protected InventoryItemRemovedEvent(string type) : base(type)
    {
    }
}