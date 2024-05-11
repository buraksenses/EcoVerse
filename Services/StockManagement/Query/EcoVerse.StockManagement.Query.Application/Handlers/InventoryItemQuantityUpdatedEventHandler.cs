using EcoVerse.Shared.Exceptions;
using EcoVerse.StockManagement.Query.Application.Notifications;
using EcoVerse.StockManagement.Query.Domain.Repositories;
using MediatR;

namespace EcoVerse.StockManagement.Query.Application.Handlers;

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
            throw new InventoryItemNotFoundException("Inventory item not found!");

        item.Quantity = notification.Quantity;
        await _repository.UpdateAsync(item);
    }
}