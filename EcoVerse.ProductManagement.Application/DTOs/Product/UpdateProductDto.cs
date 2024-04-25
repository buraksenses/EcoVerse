namespace EcoVerse.ProductManagement.Application.DTOs.Product;

public record UpdateProductDto(
    string Name, 
    int Quantity, 
    decimal price,
    string Description 
);