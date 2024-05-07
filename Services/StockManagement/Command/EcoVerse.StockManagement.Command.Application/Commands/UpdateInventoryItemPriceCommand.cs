using EcoVerse.Shared.DTOs;
using EcoVerse.StockManagement.Command.Application.DTOs;
using MediatR;

namespace EcoVerse.StockManagement.Command.Application.Commands;

public class UpdateInventoryItemPriceCommand : IRequest<Response<UpdateInventoryItemPriceDto>>
{
    public Guid Id { get; set; }
    public decimal Price { get; set; }
    
}