using EcoVerse.Shared.DTOs;
using EcoVerse.StockManagement.Query.Application.DTOs;
using MediatR;

namespace EcoVerse.StockManagement.Query.Application.Queries;

public class GetAllInventoryItemsByPriceQuery : IRequest<Response<List<InventoryItemDto>>>
{
    public decimal Price { get; set; }
}