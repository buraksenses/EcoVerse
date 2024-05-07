using EcoVerse.StockManagement.Query.Domain.Repositories;
using EcoVerse.StockManagement.Query.Infrastructure.Notifications;
using MediatR;

namespace EcoVerse.StockManagement.Query.Infrastructure.Handlers;

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
            throw new Exception("Inventory item not found!");

        item.Price = notification.Price;
        await _repository.UpdateAsync(item);
    }
}