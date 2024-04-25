namespace EcoVerse.ProductManagement.Application.DTOs.Category;

public record GetCategoryDto(
    string Name,
    string Description,
    Guid? ParentCategoryId
    );