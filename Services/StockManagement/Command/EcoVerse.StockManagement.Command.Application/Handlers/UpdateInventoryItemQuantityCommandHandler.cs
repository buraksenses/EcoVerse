using CQRS.Core.Handlers;
using EcoVerse.Shared.DTOs;
using EcoVerse.StockManagement.Command.Application.Commands;
using EcoVerse.StockManagement.Command.Application.DTOs;
using EcoVerse.StockManagement.Command.Domain.Aggregates;
using MediatR;

namespace EcoVerse.StockManagement.Command.Application.Handlers;

public class UpdateInventoryItemQuantityCommandHandler : IRequestHandler<UpdateInventoryItemQuantityCommand, Response<UpdateInventoryItemQuantityDto>>
{
    private readonly IEventSourcingHandler<InventoryItemAggregate> _eventSourcingHandler;

    public UpdateInventoryItemQuantityCommandHandler(IEventSourcingHandler<InventoryItemAggregate> eventSourcingHandler)
    {
        _eventSourcingHandler = eventSourcingHandler;
    }
    
    public async Task<Response<UpdateInventoryItemQuantityDto>> Handle(UpdateInventoryItemQuantityCommand request, CancellationToken cancellationToken)
    {
        var aggregate = await _eventSourcingHandler.GetByIdAsync(request.Id);
        aggregate.UpdateInventoryItemQuantity(request.Quantity);
        await _eventSourcingHandler.SaveAsync(aggregate);
        return Response<UpdateInventoryItemQuantityDto>.Success(new UpdateInventoryItemQuantityDto(request.Quantity), 204);
    }
}