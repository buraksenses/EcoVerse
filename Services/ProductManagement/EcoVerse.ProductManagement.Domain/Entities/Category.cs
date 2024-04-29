using EcoVerse.Shared.Entities;

namespace EcoVerse.ProductManagement.Domain.Entities;

public class Category : BaseEntity
{
    public string Name { get; set; }

    public string Description { get; set; }

    public Guid? ParentCategoryId { get; set; }
}