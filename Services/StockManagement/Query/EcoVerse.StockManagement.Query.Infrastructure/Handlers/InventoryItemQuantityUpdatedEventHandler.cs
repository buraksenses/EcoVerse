using EcoVerse.StockManagement.Query.Domain.Repositories;
using EcoVerse.StockManagement.Query.Infrastructure.Notifications;
using MediatR;

namespace EcoVerse.StockManagement.Query.Infrastructure.Handlers;

public class InventoryItemQuantityUpdatedEventHandler : INotificationHandler<InventoryItemQuantityUpdatedEvent>
{
    private readonly IInventoryItemRepository _repository;

    public InventoryItemQuantityUpdatedEventHandler(IInventoryItemRepository repository)
    {
        _repository = repository;
    }
    
    public async Task Handle(InventoryItemQuantityUpdatedEvent notification, CancellationToken cancellationToken)
    {
        var item = await _repository.GetByIdAsync(notification.Id);

        if (item == null)
            throw new Exception("Inventory item not found!");

        item.Quantity = notification.Quantity;
        await _repository.UpdateAsync(item);
    }
}