using EcoVerse.StockManagement.Query.Application.Notifications;
using EcoVerse.StockManagement.Query.Domain.Repositories;
using MediatR;

namespace EcoVerse.StockManagement.Query.Application.Handlers;

public class InventoryItemRemovedEventHandler : INotificationHandler<InventoryItemRemovedEvent>
{
    private readonly IInventoryItemRepository _repository;

    public InventoryItemRemovedEventHandler(IInventoryItemRepository repository)
    {
        _repository = repository;
    }
    
    public async Task Handle(InventoryItemRemovedEvent notification, CancellationToken cancellationToken)
    {
        var item = await _repository.GetByIdAsync(notification.Id);

        if (item == null)
            throw new Exception("Inventory item not found!");

        await _repository.RemoveAsync(item);
    }
}