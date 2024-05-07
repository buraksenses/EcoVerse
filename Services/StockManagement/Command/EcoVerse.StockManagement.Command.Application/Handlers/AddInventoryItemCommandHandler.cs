using CQRS.Core.Handlers;
using EcoVerse.Shared.DTOs;
using EcoVerse.StockManagement.Command.Application.Commands;
using EcoVerse.StockManagement.Command.Application.DTOs;
using EcoVerse.StockManagement.Command.Domain.Aggregates;
using MediatR;

namespace EcoVerse.StockManagement.Command.Application.Handlers;

public class AddInventoryItemCommandHandler : IRequestHandler<AddInventoryItemCommand, Response<AddInventoryItemDto>>
{
    private readonly IEventSourcingHandler<InventoryItemAggregate> _eventSourcingHandler;

    public AddInventoryItemCommandHandler(IEventSourcingHandler<InventoryItemAggregate> eventSourcingHandler)
    {
        _eventSourcingHandler = eventSourcingHandler;
    }
    
    public async Task<Response<AddInventoryItemDto>> Handle(AddInventoryItemCommand request, CancellationToken cancellationToken)
    {
        var aggregate = new InventoryItemAggregate(Guid.NewGuid(), request.ProductId, request.Name, request.Price,
            request.Description, request.Quantity);
        await _eventSourcingHandler.SaveAsync(aggregate);
        return Response<AddInventoryItemDto>.Success(
            new AddInventoryItemDto(request.ProductId, request.Name, request.Description, request.Price,
                request.Quantity), 201);
    }
}