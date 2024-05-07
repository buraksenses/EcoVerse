using MediatR;

namespace EcoVerse.StockManagement.Query.Infrastructure.Notifications;

public class InventoryItemPriceUpdatedEvent : INotification
{
    public Guid Id { get; set; }
    
    public decimal Price { get; set; }
}