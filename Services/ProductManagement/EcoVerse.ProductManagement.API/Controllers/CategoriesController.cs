﻿using EcoVerse.ProductManagement.Application.DTOs.Category;
using EcoVerse.ProductManagement.Application.Interfaces;
using EcoVerse.Shared.ControllerBases;
using Microsoft.AspNetCore.Mvc;

namespace EcoVerse.ProductManagement.API.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class CategoriesController : CustomBaseController
{
    private readonly ICategoryService _categoryService;

    public CategoriesController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateAsync(CreateCategoryDto categoryDto) 
        => CreateActionResultInstance(await _categoryService.CreateAsync(categoryDto));

    [HttpGet]
    public async Task<IActionResult> ListAllAsync(string? filterOn = null, string? filterQuery = null,string?
            sortBy = null,bool? isAscending = null,
        int pageNumber = 1, int pageSize = 1000)
    {
        return CreateActionResultInstance(await _categoryService.ListAllAsync(filterOn, filterQuery, sortBy, isAscending,
            pageNumber, pageSize));
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateAsync(Guid id, UpdateCategoryDto updateCategoryDto) 
        => CreateActionResultInstance(await _categoryService.UpdateAsync(id, updateCategoryDto));
    
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteAsync(Guid id) 
        => CreateActionResultInstance(await _categoryService.DeleteAsync(id));
    
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetByIdAsync(Guid id) 
        => CreateActionResultInstance(await _categoryService.GetByIdAsync(id));
}