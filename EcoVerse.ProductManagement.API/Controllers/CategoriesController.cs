using EcoVerse.ProductManagement.Application.DTOs.Category;
using EcoVerse.ProductManagement.Application.Interfaces;
using EcoVerse.Shared.ControllerBases;
using Microsoft.AspNetCore.Mvc;

namespace EcoVerse.ProductManagement.API.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class CategoriesController : CustomBaseController
{
    private readonly ILogger<ProductsController> _logger;
    private readonly ICategoryService _categoryService;

    public CategoriesController(ILogger<ProductsController> logger, ICategoryService categoryService)
    {
        _logger = logger;
        _categoryService = categoryService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync(CreateCategoryDto categoryDto)
    {
        var response = await _categoryService.CreateAsync(categoryDto);

        return CreateActionResultInstance(response);
    }
}