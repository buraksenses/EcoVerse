using EcoVerse.ProductManagement.Application.DTOs;
using EcoVerse.ProductManagement.Application.Interfaces;
using EcoVerse.Shared.ControllerBases;
using Microsoft.AspNetCore.Mvc;

namespace EcoVerse.ProductManagement.API.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class ProductsController : CustomBaseController
{
    private readonly ILogger<ProductsController> _logger;
    private readonly IProductService _productService;

    public ProductsController(ILogger<ProductsController> logger, IProductService productService)
    {
        _logger = logger;
        _productService = productService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync(CreateProductDto productDto)
    {
        var response = await _productService.CreateAsync(productDto);

        return CreateActionResultInstance(response);
    }
}