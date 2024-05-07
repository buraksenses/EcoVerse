using MediatR;

namespace EcoVerse.StockManagement.Query.Infrastructure.Notifications;

public class InventoryItemQuantityUpdatedEvent : INotification
{
    public Guid Id { get; set; }

    public int Quantity { get; set; }
}