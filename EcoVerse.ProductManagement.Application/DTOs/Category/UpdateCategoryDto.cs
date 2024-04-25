namespace EcoVerse.ProductManagement.Application.DTOs.Category;

public record UpdateCategoryDto(
    string Name,
    string Description,
    Guid? ParentCategoryId
);