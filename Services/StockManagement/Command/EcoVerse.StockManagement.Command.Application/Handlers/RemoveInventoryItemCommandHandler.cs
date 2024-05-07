using CQRS.Core.Handlers;
using EcoVerse.Shared.DTOs;
using EcoVerse.StockManagement.Command.Application.Commands;
using EcoVerse.StockManagement.Command.Application.DTOs;
using EcoVerse.StockManagement.Command.Domain.Aggregates;
using MediatR;

namespace EcoVerse.StockManagement.Command.Application.Handlers;

public class RemoveInventoryItemCommandHandler : IRequestHandler<RemoveInventoryItemCommand, Response<RemoveInventoryItemDto>>
{
    private readonly IEventSourcingHandler<InventoryItemAggregate> _eventSourcingHandler;

    public RemoveInventoryItemCommandHandler(IEventSourcingHandler<InventoryItemAggregate> eventSourcingHandler)
    {
        _eventSourcingHandler = eventSourcingHandler;
    }
    
    public async Task<Response<RemoveInventoryItemDto>> Handle(RemoveInventoryItemCommand request, CancellationToken cancellationToken)
    {
        var aggregate = await _eventSourcingHandler.GetByIdAsync(request.Id);
        aggregate.RemoveInventoryItem(request.Id);
        await _eventSourcingHandler.SaveAsync(aggregate);
        return Response<RemoveInventoryItemDto>.Success(new RemoveInventoryItemDto(request.Id), 204);
    }
}