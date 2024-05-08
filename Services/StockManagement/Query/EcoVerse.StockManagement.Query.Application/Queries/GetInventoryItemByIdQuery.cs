using EcoVerse.Shared.DTOs;
using EcoVerse.StockManagement.Query.Application.DTOs;
using MediatR;

namespace EcoVerse.StockManagement.Query.Application.Queries;

public class GetInventoryItemByIdQuery : IRequest<Response<InventoryItemDto>>
{
    public Guid Id { get; set; }
}