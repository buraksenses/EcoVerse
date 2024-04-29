namespace EcoVerse.ProductManagement.Application.DTOs.Product;

public record CreateProductDto(
    string Name, 
    int Quantity, 
    decimal Price,
    string Description,
    Guid CategoryId
    );