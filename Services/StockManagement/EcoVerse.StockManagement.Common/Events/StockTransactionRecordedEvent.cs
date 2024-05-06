using CQRS.Core.Events;

namespace EcoVerse.StockManagement.Common.Events;

public class StockTransactionRecordedEvent : BaseEvent
{
    protected StockTransactionRecordedEvent(string type) : base(type)
    {
    }
}