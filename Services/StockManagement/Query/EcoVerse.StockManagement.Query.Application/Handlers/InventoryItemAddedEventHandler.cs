using EcoVerse.StockManagement.Query.Application.Notifications;
using EcoVerse.StockManagement.Query.Domain.Entities;
using EcoVerse.StockManagement.Query.Domain.Repositories;
using MediatR;

namespace EcoVerse.StockManagement.Query.Application.Handlers;

public class InventoryItemAddedEventHandler : INotificationHandler<InventoryItemAddedEvent>
{
    private readonly IInventoryItemRepository _repository;

    public InventoryItemAddedEventHandler(IInventoryItemRepository repository)
    {
        _repository = repository;
    }

    public async Task Handle(InventoryItemAddedEvent notification, CancellationToken cancellationToken)
    {
        await _repository.AddAsync(new InventoryItemEntity
        {
            Id = notification.Id,
            ProductId = notification.ProductId,
            Name = notification.Name,
            Quantity = notification.Quantity,
            Price = notification.Price,
            Description = notification.Description
        });
    }
}