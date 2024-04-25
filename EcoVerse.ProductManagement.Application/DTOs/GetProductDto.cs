namespace EcoVerse.ProductManagement.Application.DTOs;

public record GetProductDto(
    string Name, 
    int Quantity, 
    decimal price,
    string Description,
    Guid CategoryId
);