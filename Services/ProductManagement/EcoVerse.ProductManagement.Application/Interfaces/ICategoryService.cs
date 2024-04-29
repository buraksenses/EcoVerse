using EcoVerse.ProductManagement.Application.DTOs.Category;
using EcoVerse.Shared.DTOs;

namespace EcoVerse.ProductManagement.Application.Interfaces;

public interface ICategoryService
{
    Task<Response<NoContent>> CreateAsync(CreateCategoryDto productDto);

    Task<Response<List<GetCategoryDto>>> ListAllAsync(string? filterOn = null, string? filterQuery = null,string?
            sortBy = null,bool? isAscending = null,
        int pageNumber = 1, int pageSize = 1000);

    Task<Response<NoContent>> UpdateAsync(Guid id, UpdateCategoryDto productDto);

    Task<Response<NoContent>> DeleteAsync(Guid id);

    Task<Response<GetCategoryDto>> GetByIdAsync(Guid id);
}