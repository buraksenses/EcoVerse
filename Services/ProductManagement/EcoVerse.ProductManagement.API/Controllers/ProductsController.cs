using EcoVerse.ProductManagement.Application.DTOs.Product;
using EcoVerse.ProductManagement.Application.Interfaces;
using EcoVerse.Shared.ControllerBases;
using EcoVerse.Shared.Messages;
using MassTransit;
using Microsoft.AspNetCore.Authorization;
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
        => CreateActionResultInstance(await _productService.CreateAsync(productDto));
    
    [HttpGet]
    public async Task<IActionResult> ListAllAsync(string? filterOn = null, string? filterQuery = null,string?
            sortBy = null,bool? isAscending = null,
        int pageNumber = 1, int pageSize = 1000)
    {
        return CreateActionResultInstance(await _productService.ListAllAsync(filterOn, filterQuery, sortBy, isAscending,
            pageNumber, pageSize));
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetByIdAsync(Guid id) 
        => CreateActionResultInstance(await _productService.GetByIdAsync(id));
    
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateAsync(Guid id, UpdateProductDto updateProductDto) 
        => CreateActionResultInstance(await _productService.UpdateAsync(id, updateProductDto));
    
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteAsync(Guid id) 
        => CreateActionResultInstance(await _productService.DeleteAsync(id));

    [HttpGet("categories/{categoryId:guid}")]
    public async Task<IActionResult> GetByCategory(Guid categoryId) 
        => CreateActionResultInstance(await _productService.GetByCategory(categoryId));
}