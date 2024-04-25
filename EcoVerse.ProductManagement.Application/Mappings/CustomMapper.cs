using AutoMapper;
using EcoVerse.ProductManagement.Application.DTOs.Category;
using EcoVerse.ProductManagement.Application.DTOs.Product;
using EcoVerse.ProductManagement.Domain.Entities;

namespace EcoVerse.ProductManagement.Application.Mappings;

public class CustomMapper : Profile
{
    public CustomMapper()
    {
        CreateMap<Product, CreateProductDto>().ReverseMap();
        CreateMap<Product, GetProductDto>().ReverseMap();
        CreateMap<Product, UpdateProductDto>().ReverseMap();
        CreateMap<Product, DeleteProductDto>().ReverseMap();
        
        CreateMap<Category, CreateCategoryDto>().ReverseMap();
        CreateMap<Category, GetCategoryDto>().ReverseMap();
        CreateMap<Category, UpdateCategoryDto>().ReverseMap();
        CreateMap<Category, DeleteCategoryDto>().ReverseMap();
    }
}