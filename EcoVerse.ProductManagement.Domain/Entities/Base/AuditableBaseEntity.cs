namespace EcoVerse.ProductManagement.Domain.Entities.Base;

public abstract class AuditableBaseEntity : BaseEntity
{
    public DateTime CreatedDate { get; set; }
}