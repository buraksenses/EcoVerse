using CQRS.Core.Domain;
using EcoVerse.StockManagement.Common.Events;

namespace EcoVerse.StockManagement.Command.Domain.Aggregates;

public class InventoryItemAggregate : AggregateRoot
{
    private Guid _productId;

    private string _name;

    private decimal _price;

    private int _quantity;
    
    private string _description;

    public InventoryItemAggregate()
    {
        
    }

    #region Add Item

    public InventoryItemAggregate(Guid id, Guid productId, string name, decimal price, string description, int quantity)
    {
       RaiseEvent(new InventoryItemAddedEvent
       {
           Id = id,
           Description = description,
           Name = name,
           Price = price,
           ProductId = productId,
           Quantity = quantity
       });
    }
    
    public void Apply(InventoryItemAddedEvent @event)
    {
        _id = @event.Id;
        _description = @event.Description;
        _name = @event.Name;
        _price = @event.Price;
        _productId = @event.ProductId;
    }
    
    #endregion

    #region Update Item Quantity

    public void UpdateInventoryItemQuantity(int quantity)
    {
        if (quantity < 0)
            throw new InvalidOperationException("Quantity cannot be less than zero!");
        
        RaiseEvent(new InventoryItemQuantityUpdatedEvent
        {
            Id = Id,
            Quantity = quantity
        });
    }

    public void Apply(InventoryItemQuantityUpdatedEvent @event)
    {
        _id = @event.Id;
    }

    #endregion
    
    #region Update Item Price

    public void UpdateInventoryItemPrice(decimal price)
    {
        if (price < 0m)
            throw new InvalidOperationException("Price cannot be less than zero!");
        
        RaiseEvent(new InventoryItemPriceUpdatedEvent
        {
            Id = Id,
            Price = price
        });
    }

    public void Apply(InventoryItemPriceUpdatedEvent @event)
    {
        _id = @event.Id;
    }

    #endregion
    
    #region Remove Item

    public void RemoveInventoryItem(Guid itemId)
    {
        RaiseEvent(new InventoryItemRemovedEvent
        {
            Id = Id,
            ItemId = itemId
        });
    }

    public void Apply(InventoryItemRemovedEvent @event)
    {
        _id = @event.Id;
    }

    #endregion
}