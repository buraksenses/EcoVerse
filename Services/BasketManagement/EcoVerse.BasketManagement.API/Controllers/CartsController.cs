using EcoVerse.BasketManagement.Application.DTOs;
using EcoVerse.BasketManagement.Application.Interfaces;
using EcoVerse.Shared.ControllerBases;
using EcoVerse.Shared.DTOs;
using EcoVerse.Shared.Messages;
using EcoVerse.Shared.Services;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

namespace EcoVerse.BasketManagement.API.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class CartsController : CustomBaseController
{
    private readonly ICartService _cartService;
    private readonly ISharedIdentityService _sharedIdentityService;
    private readonly IPublishEndpoint _publishEndpoint;

    public CartsController(ICartService cartService, ISharedIdentityService sharedIdentityService, IPublishEndpoint publishEndpoint)
    {
        _cartService = cartService;
        _sharedIdentityService = sharedIdentityService;
        _publishEndpoint = publishEndpoint;
    }

    [HttpGet]
    public async Task<IActionResult> GetByUserIdAsync() 
        => CreateActionResultInstance(await _cartService.GetByUserId(_sharedIdentityService.GetUserId));

    [HttpPost("items")]
    public async Task<IActionResult> AddItemAsync(AddToCartDto addToCartDto)
    {
        await _publishEndpoint.Publish<AddItemToCartCommand>(new AddItemToCartCommand
        {
            Price = addToCartDto.Price,
            ProductId = addToCartDto.ProductId,
            Quantity = addToCartDto.Quantity,
            UserId = _sharedIdentityService.GetUserId
        });
        
        return CreateActionResultInstance(Shared.DTOs.Response<NoContent>.Success(204));
    }

    [HttpPut("items/{itemId}")]
    public async Task<IActionResult> UpdateQuantityAsync(Guid itemId, UpdateCartDto updateCartDto)
        => CreateActionResultInstance(
            await _cartService.UpdateQuantityAsync(_sharedIdentityService.GetUserId, itemId, updateCartDto));

    [HttpDelete("items")]
    public async Task<IActionResult> DeleteItemAsync(DeleteFromCartDto deleteFromCartDto)
        => CreateActionResultInstance(
            await _cartService.DeleteItemAsync(_sharedIdentityService.GetUserId, deleteFromCartDto));
}