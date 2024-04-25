using EcoVerse.ProductManagement.Domain.Entities.Base;

namespace EcoVerse.ProductManagement.Domain.Entities;

public class Price : BaseEntity
{
    public decimal Amount { get; set; }

    public DateTime EndDate { get; set; }

    public Guid ProductId { get; set; }
}