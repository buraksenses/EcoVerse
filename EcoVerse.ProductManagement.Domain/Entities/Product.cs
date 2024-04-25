using EcoVerse.ProductManagement.Domain.Entities.Base;

namespace EcoVerse.ProductManagement.Domain.Entities;

public class Product : AuditableBaseEntity
{
    public string Name { get; set; }

    public int Quantity { get; set; }

    public decimal Price { get; set; }

    public string Description { get; set; }

    public bool IsActive { get; set; }

    public Guid CategoryId { get; set; }
    
    //Navigation Properties

    public virtual Category Category { get; set; }
}