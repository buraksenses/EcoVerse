using EcoVerse.BasketManagement.Application.DTOs;
using EcoVerse.BasketManagement.Application.Interfaces;
using EcoVerse.BasketManagement.Domain.Entities;
using EcoVerse.Shared.ControllerBases;
using EcoVerse.Shared.Services;
using Microsoft.AspNetCore.Mvc;

namespace EcoVerse.BasketManagement.API.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class CartsController : CustomBaseController
{
    private readonly ICartService _cartService;
    private readonly ISharedIdentityService _sharedIdentityService;

    public CartsController(ICartService cartService, ISharedIdentityService sharedIdentityService)
    {
        _cartService = cartService;
        _sharedIdentityService = sharedIdentityService;
    }

    [HttpGet]
    public async Task<IActionResult> GetByUserIdAsync() 
        => CreateActionResultInstance(await _cartService.GetByUserId(_sharedIdentityService.GetUserId));

    [HttpPost("items")]
    public async Task<IActionResult> AddItemAsync(AddToCartDto addToCartDto)
        => CreateActionResultInstance(await _cartService.AddItemAsync(_sharedIdentityService.GetUserId,
            addToCartDto));

    [HttpPut("items/{itemId}")]
    public async Task<IActionResult> UpdateQuantityAsync(Guid itemId, UpdateCartDto updateCartDto)
        => CreateActionResultInstance(
            await _cartService.UpdateQuantityAsync(_sharedIdentityService.GetUserId, itemId, updateCartDto));

    [HttpDelete("items")]
    public async Task<IActionResult> DeleteItemAsync(DeleteFromCartDto deleteFromCartDto)
        => CreateActionResultInstance(
            await _cartService.DeleteItemAsync(_sharedIdentityService.GetUserId, deleteFromCartDto));
}