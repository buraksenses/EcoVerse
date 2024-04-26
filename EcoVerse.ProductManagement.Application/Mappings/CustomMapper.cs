using AutoMapper;
using EcoVerse.ProductManagement.Application.DTOs.Category;
using EcoVerse.ProductManagement.Application.DTOs.Product;
using EcoVerse.ProductManagement.Domain.Entities;

namespace EcoVerse.ProductManagement.Application.Mappings;

public class CustomMapper : Profile
{
    public CustomMapper()
    {
        CreateMap<Product, CreateProductDto>()
            .ReverseMap()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        
        CreateMap<Product, GetProductDto>()
            .ReverseMap()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

        CreateMap<Product, UpdateProductDto>()
            .ReverseMap()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

        CreateMap<Category, CreateCategoryDto>()
            .ReverseMap()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        
        CreateMap<Category, GetCategoryDto>()
            .ReverseMap()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        
        CreateMap<Category, UpdateCategoryDto>()
            .ReverseMap()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        
        CreateMap<Category, DeleteCategoryDto>()
            .ReverseMap()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
    }
}