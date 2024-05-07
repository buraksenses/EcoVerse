using EcoVerse.Shared.DTOs;
using EcoVerse.StockManagement.Command.Application.DTOs;
using MediatR;

namespace EcoVerse.StockManagement.Command.Application.Commands;

public class RemoveInventoryItemCommand : IRequest<Response<RemoveInventoryItemDto>>
{
    public Guid Id { get; set; }
}