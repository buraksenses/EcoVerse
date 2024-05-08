using MediatR;

namespace EcoVerse.StockManagement.Query.Application.Notifications;

public class InventoryItemAddedEvent : INotification
{
    public Guid Id { get; set; }
    public Guid ProductId { get; set; }
    public string Name { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }

    public string Description { get; set; }
}