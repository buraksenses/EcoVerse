using AutoMapper;
using EcoVerse.StockManagement.Query.Application.DTOs;
using EcoVerse.StockManagement.Query.Domain.Entities;

namespace EcoVerse.StockManagement.Query.Application.Mappings;

public class CustomMapper : Profile
{
    public CustomMapper()
    {
        CreateMap<InventoryItemEntity, InventoryItemDto>().ReverseMap();
    }
}