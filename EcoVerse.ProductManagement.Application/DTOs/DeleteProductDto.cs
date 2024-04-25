namespace EcoVerse.ProductManagement.Application.DTOs;

public record DeleteProductDto(
    string Name, 
    int Quantity, 
    decimal price,
    string Description
);