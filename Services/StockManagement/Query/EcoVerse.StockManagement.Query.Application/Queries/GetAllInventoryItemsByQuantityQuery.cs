using EcoVerse.Shared.DTOs;
using EcoVerse.StockManagement.Query.Application.DTOs;
using MediatR;

namespace EcoVerse.StockManagement.Query.Application.Queries;

public class GetAllInventoryItemsByQuantityQuery : IRequest<Response<List<InventoryItemDto>>>
{
    public int Quantity { get; set; }
}