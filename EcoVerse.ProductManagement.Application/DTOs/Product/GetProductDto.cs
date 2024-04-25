namespace EcoVerse.ProductManagement.Application.DTOs.Product;

public record GetProductDto(
    string Name, 
    int Quantity, 
    decimal price,
    string Description,
    Guid CategoryId
);