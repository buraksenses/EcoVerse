using CQRS.Core.Handlers;
using EcoVerse.Shared.DTOs;
using EcoVerse.StockManagement.Command.Application.Commands;
using EcoVerse.StockManagement.Command.Application.DTOs;
using EcoVerse.StockManagement.Command.Domain.Aggregates;
using MediatR;

namespace EcoVerse.StockManagement.Command.Application.Handlers;

public class UpdateInventoryItemPriceCommandHandler : IRequestHandler<UpdateInventoryItemPriceCommand, Response<UpdateInventoryItemPriceDto>>
{
    private readonly IEventSourcingHandler<InventoryItemAggregate> _eventSourcingHandler;

    public UpdateInventoryItemPriceCommandHandler(IEventSourcingHandler<InventoryItemAggregate> eventSourcingHandler)
    {
        _eventSourcingHandler = eventSourcingHandler;
    }
    
    public async Task<Response<UpdateInventoryItemPriceDto>> Handle(UpdateInventoryItemPriceCommand request, CancellationToken cancellationToken)
    {
        var aggregate = await _eventSourcingHandler.GetByIdAsync(request.Id);
        aggregate.UpdateInventoryItemPrice(request.Price);
        await _eventSourcingHandler.SaveAsync(aggregate);
        return Response<UpdateInventoryItemPriceDto>.Success(new UpdateInventoryItemPriceDto(request.Price), 204);
    }
}