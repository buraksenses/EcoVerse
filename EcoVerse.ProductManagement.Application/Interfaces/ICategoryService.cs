using EcoVerse.ProductManagement.Application.DTOs.Category;
using EcoVerse.Shared.DTOs;

namespace EcoVerse.ProductManagement.Application.Interfaces;

public interface ICategoryService
{
    Task<Response<NoContent>> CreateAsync(CreateCategoryDto productDto);

    Task<Response<List<GetCategoryDto>>> ListAllAsync();

    Task<Response<NoContent>> UpdateAsync(Guid id, UpdateCategoryDto productDto);

    Task<Response<NoContent>> DeleteAsync(Guid id);

    Task<Response<GetCategoryDto>> GetByIdAsync(Guid id);
}