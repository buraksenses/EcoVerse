using EcoVerse.ProductManagement.Application.DTOs;
using EcoVerse.ProductManagement.Application.DTOs.Product;
using EcoVerse.Shared.DTOs;

namespace EcoVerse.ProductManagement.Application.Interfaces;

public interface IProductService
{
    Task<Response<NoContent>> CreateAsync(CreateProductDto productDto);

    Task<Response<List<GetProductDto>>> ListAllAsync(string? filterOn = null, string? filterQuery = null,string?
            sortBy = null,bool? isAscending = null,
        int pageNumber = 1, int pageSize = 1000);

    Task<Response<NoContent>> UpdateAsync(Guid id, UpdateProductDto productDto);

    Task<Response<NoContent>> DeleteAsync(Guid id);

    Task<Response<GetProductDto>> GetByIdAsync(Guid id);

    Task<Response<List<GetProductDto>>> GetByCategory(Guid categoryId);
}