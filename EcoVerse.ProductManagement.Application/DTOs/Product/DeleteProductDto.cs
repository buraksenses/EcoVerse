namespace EcoVerse.ProductManagement.Application.DTOs.Product;

public record DeleteProductDto(
    string Name, 
    int Quantity, 
    decimal price,
    string Description
);