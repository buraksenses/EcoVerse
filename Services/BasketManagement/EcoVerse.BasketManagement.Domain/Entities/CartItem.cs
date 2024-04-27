using EcoVerse.Shared.Entities;

namespace EcoVerse.BasketManagement.Domain.Entities;

public class CartItem : BaseEntity
{
    public Guid ProductId { get; set; }

    public int Quantity { get; set; }

    public decimal Price { get; set; }
}