using EcoVerse.Shared.ControllerBases;
using EcoVerse.StockManagement.Query.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EcoVerse.StockManagement.Query.API.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class InventoryItemsController : CustomBaseController
{
    private readonly IMediator _mediator;

    public InventoryItemsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        var response = await _mediator.Send(new GetAllInventoryItemsQuery());
        return CreateActionResultInstance(response);
    }
    
    [HttpGet("byId/{id}")]
    public async Task<IActionResult> GetByIdAsync(Guid id)
    {
        var response = await _mediator.Send(new GetInventoryItemByIdQuery{Id = id});
        return CreateActionResultInstance(response);
    }
    
    [HttpGet("byQuantity/{quantity}")]
    public async Task<IActionResult> GetAllByQuantityAsync(int quantity)
    {
        var response = await _mediator.Send(new GetAllInventoryItemsByQuantityQuery{Quantity = quantity});
        return CreateActionResultInstance(response);
    }
    
    [HttpGet("byPrice/{price}")]
    public async Task<IActionResult> GetAllByPriceAsync(decimal price)
    {
        var response = await _mediator.Send(new GetAllInventoryItemsByPriceQuery { Price = price });
        return CreateActionResultInstance(response);
    }
}