using MediatR;

namespace EcoVerse.StockManagement.Query.Application.Notifications;

public class InventoryItemQuantityUpdatedEvent : INotification
{
    public Guid Id { get; set; }

    public int Quantity { get; set; }
}