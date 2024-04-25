namespace EcoVerse.ProductManagement.Application.DTOs.Category;

public record DeleteCategoryDto(
    string Name,
    string Description,
    Guid? ParentCategoryId
    );