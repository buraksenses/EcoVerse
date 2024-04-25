namespace EcoVerse.ProductManagement.Application.DTOs;

public record UpdateProductDto(
    string Name, 
    int Quantity, 
    decimal price,
    string Description 
);