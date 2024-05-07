using MediatR;

namespace EcoVerse.StockManagement.Query.Infrastructure.Notifications;

public class InventoryItemRemovedEvent : INotification
{
    public Guid Id { get; set; }
}