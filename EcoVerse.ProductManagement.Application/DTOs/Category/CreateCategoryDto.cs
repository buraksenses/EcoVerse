namespace EcoVerse.ProductManagement.Application.DTOs.Category;

public record CreateCategoryDto(
    string Name,
    string Description,
    Guid? ParentCategoryId
    );