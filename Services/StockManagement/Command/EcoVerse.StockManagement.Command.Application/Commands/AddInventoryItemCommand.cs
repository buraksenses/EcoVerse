using EcoVerse.Shared.DTOs;
using EcoVerse.StockManagement.Command.Application.DTOs;
using MediatR;

namespace EcoVerse.StockManagement.Command.Application.Commands;

public class AddInventoryItemCommand : IRequest<Response<AddInventoryItemDto>>
{
    public Guid ProductId { get; set; }

    public string Name { get; set; }

    public decimal Price { get; set; }

    public string Description { get; set; }

    public int Quantity { get; set; }
}