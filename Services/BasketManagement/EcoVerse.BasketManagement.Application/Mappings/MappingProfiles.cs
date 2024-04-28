using AutoMapper;
using EcoVerse.BasketManagement.Application.DTOs;
using EcoVerse.BasketManagement.Domain.Entities;

namespace EcoVerse.BasketManagement.Application.Mappings;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<AddToCartDto, Cart>().ReverseMap();
        CreateMap<DeleteFromCartDto, Cart>().ReverseMap();
        CreateMap<UpdateCartDto, Cart>().ReverseMap();
        CreateMap<GetCartDto, Cart>().ReverseMap();
    }
}