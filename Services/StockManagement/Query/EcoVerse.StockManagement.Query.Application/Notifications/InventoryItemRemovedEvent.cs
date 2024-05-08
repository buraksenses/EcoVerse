using MediatR;

namespace EcoVerse.StockManagement.Query.Application.Notifications;

public class InventoryItemRemovedEvent : INotification
{
    public Guid Id { get; set; }
}