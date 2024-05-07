using EcoVerse.Shared.DTOs;
using EcoVerse.StockManagement.Command.Application.DTOs;
using MediatR;

namespace EcoVerse.StockManagement.Command.Application.Commands;

public class UpdateInventoryItemQuantityCommand : IRequest<Response<UpdateInventoryItemQuantityDto>>
{
    public Guid Id { get; set; }
    
    public int Quantity { get; set; }
}