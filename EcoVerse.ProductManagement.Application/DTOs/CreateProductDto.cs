namespace EcoVerse.ProductManagement.Application.DTOs;

public record CreateProductDto(
    string Name, 
    int Quantity, 
    decimal price,
    string Description,
    Guid CategoryId
    );