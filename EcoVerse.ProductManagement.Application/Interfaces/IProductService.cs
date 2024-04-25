using EcoVerse.ProductManagement.Application.DTOs;
using EcoVerse.Shared.DTOs;

namespace EcoVerse.ProductManagement.Application.Interfaces;

public interface IProductService
{
    Task<Response<NoContent>> CreateAsync(CreateProductDto productDto);

    Task<Response<List<GetProductDto>>> ListAllAsync();

    Task<Response<NoContent>> UpdateAsync(Guid id, UpdateProductDto productDto);

    Task<Response<NoContent>> DeleteAsync(Guid id);

    Task<Response<GetProductDto>> GetByIdAsync(Guid id);
}