using EcoVerse.Shared.Exceptions;
using EcoVerse.StockManagement.Query.Application.Notifications;
using EcoVerse.StockManagement.Query.Domain.Repositories;
using MediatR;

namespace EcoVerse.StockManagement.Query.Application.Handlers;

public class InventoryItemPriceUpdatedEventHandler : INotificationHandler<InventoryItemPriceUpdatedEvent>
{
    private readonly IInventoryItemRepository _repository;

    public InventoryItemPriceUpdatedEventHandler(IInventoryItemRepository repository)
    {
        _repository = repository;
    }
    
    public async Task Handle(InventoryItemPriceUpdatedEvent notification, CancellationToken cancellationToken)
    {
        var item = await _repository.GetByIdAsync(notification.Id);

        if (item == null)
            throw new InventoryItemNotFoundException("Inventory item not found!");

        item.Price = notification.Price;
        await _repository.UpdateAsync(item);
    }
}