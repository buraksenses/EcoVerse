using EcoVerse.Shared.Entities;

namespace EcoVerse.BasketManagement.Domain.Entities;

public class Cart : AuditableBaseEntity
{
    public decimal TotalAmount { get; set; }

    public string UserId { get; set; }

    public virtual ICollection<CartItem> CartItems { get; set; }

    public decimal TotalPrice => CartItems.Sum(x => x.Price * x.Quantity);
}