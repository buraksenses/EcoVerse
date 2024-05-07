using EcoVerse.Shared.ControllerBases;
using EcoVerse.StockManagement.Command.Application.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EcoVerse.StockManagement.Command.API.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class InventoryItemsController : CustomBaseController
{
    private readonly IMediator _mediator;

    public InventoryItemsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> AddInventoryItemAsync(AddInventoryItemCommand command)
    {
        var response = await _mediator.Send(command);
        return CreateActionResultInstance(response);
    }

    [HttpPut("{id}/quantity")]
    public async Task<IActionResult> UpdateInventoryItemQuantityAsync(Guid id, UpdateInventoryItemQuantityCommand command)
    {
        command.Id = id;
        var response = await _mediator.Send(command);
        return CreateActionResultInstance(response);
    }
    
    [HttpPut("{id}/price")]
    public async Task<IActionResult> UpdateInventoryItemPriceAsync(Guid id, UpdateInventoryItemPriceCommand command)
    {
        command.Id = id;
        var response = await _mediator.Send(command);
        return CreateActionResultInstance(response);
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> RemoveInventoryItemAsync(Guid id, RemoveInventoryItemCommand command)
    {
        command.Id = id;
        var response = await _mediator.Send(command);
        return CreateActionResultInstance(response);
    }
}