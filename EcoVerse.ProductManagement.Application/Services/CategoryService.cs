using EcoVerse.ProductManagement.Application.DTOs.Category;
using EcoVerse.ProductManagement.Application.Interfaces;
using EcoVerse.ProductManagement.Application.Mappings;
using EcoVerse.ProductManagement.Domain.Entities;
using EcoVerse.ProductManagement.Domain.Interfaces;
using EcoVerse.Shared.DTOs;

namespace EcoVerse.ProductManagement.Application.Services;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;
    
    public CategoryService(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }
    
    public async Task<Response<NoContent>> CreateAsync(CreateCategoryDto categoryDto)
    {
        var category = ObjectMapper.Mapper.Map<Category>(categoryDto);
        
        if(category == null)
            return Response<NoContent>.Fail("Category can not be null!", 400);

        await _categoryRepository.CreateAsync(category);
        
        return Response<NoContent>.Success(201);
    }

    public async Task<Response<List<GetCategoryDto>>> ListAllAsync(string? filterOn = null, string? filterQuery = null,string?
            sortBy = null,bool? isAscending = null,
        int pageNumber = 1, int pageSize = 1000)
    {
        var categories = await _categoryRepository.ListAllAsync(filterOn, filterQuery, sortBy, isAscending,
            pageNumber, pageSize);

        var categoryListDto = ObjectMapper.Mapper.Map<List<GetCategoryDto>>(categories);
        
        return Response<List<GetCategoryDto>>.Success(categoryListDto,200);
    }

    public async Task<Response<NoContent>> UpdateAsync(Guid id, UpdateCategoryDto categoryDto)
    {
        var existingCategory = await _categoryRepository.GetByIdAsync(id);
        
        if(existingCategory == null)
            return Response<NoContent>.Fail("Could not found category with given ID!",404);

        existingCategory = ObjectMapper.Mapper.Map<Category>(categoryDto);

        await _categoryRepository.UpdateAsync(existingCategory);
        
        return Response<NoContent>.Success(204);
    }

    public async Task<Response<NoContent>> DeleteAsync(Guid id)
    {
        var existingCategory = await _categoryRepository.GetByIdAsync(id);
        
        if(existingCategory == null)
            return Response<NoContent>.Fail("Could not found category with given ID!",404);

        await _categoryRepository.DeleteAsync(existingCategory);
        
        return Response<NoContent>.Success(200);
    }

    public async Task<Response<GetCategoryDto>> GetByIdAsync(Guid id)
    {
        var category = await _categoryRepository.GetByIdAsync(id);
        
        return category == null 
            ? Response<GetCategoryDto>.Fail("Could not found product with given ID!",404) 
            : Response<GetCategoryDto>.Success(ObjectMapper.Mapper.Map<GetCategoryDto>(category),200);
    }
}