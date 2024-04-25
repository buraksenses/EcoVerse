using EcoVerse.ProductManagement.Domain.Entities.Base;

namespace EcoVerse.ProductManagement.Domain.Entities;

public class Category : BaseEntity
{
    public string Name { get; set; }

    public string Description { get; set; }

    public Guid? ParentCategoryId { get; set; }
}