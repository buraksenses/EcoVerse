using EcoVerse.ProductManagement.Application.DTOs.Product;
using EcoVerse.ProductManagement.Application.Interfaces;
using EcoVerse.ProductManagement.Application.Mappings;
using EcoVerse.ProductManagement.Domain.Entities;
using EcoVerse.ProductManagement.Domain.Exceptions;
using EcoVerse.ProductManagement.Domain.Interfaces;
using EcoVerse.Shared.DTOs;

namespace EcoVerse.ProductManagement.Application.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;

    public ProductService(IProductRepository productRepository) => _productRepository = productRepository;
    
    public async Task<Response<NoContent>> CreateAsync(CreateProductDto productDto)
    {
        var product = ObjectMapper.Mapper.Map<Product>(productDto);
        
        IsValid(product);

        await _productRepository.CreateAsync(product);
        
        return Response<NoContent>.Success(201);
    }

    public async Task<Response<List<GetProductDto>>> ListAllAsync(string? filterOn = null, string? filterQuery = null,string?
            sortBy = null,bool? isAscending = null,
        int pageNumber = 1, int pageSize = 1000)
    {
        var products = await _productRepository.ListAllAsync(filterOn, filterQuery, sortBy, isAscending,
            pageNumber, pageSize);

        var productListDto = ObjectMapper.Mapper.Map<List<GetProductDto>>(products);
        
        return Response<List<GetProductDto>>.Success(productListDto,200);
    }

    public async Task<Response<NoContent>> UpdateAsync(Guid id, UpdateProductDto productDto)
    {
        var existingProduct = await _productRepository.GetByIdAsync(id);
        
        IsValid(existingProduct);

        ObjectMapper.Mapper.Map(productDto, existingProduct);

        await _productRepository.UpdateAsync(existingProduct);
        
        return Response<NoContent>.Success(204);
    }

    public async Task<Response<NoContent>> DeleteAsync(Guid id)
    {
        var existingProduct = await _productRepository.GetByIdAsync(id);
        
        IsValid(existingProduct);

        await _productRepository.DeleteAsync(existingProduct);
        
        return Response<NoContent>.Success(200);
    }

    public async Task<Response<GetProductDto>> GetByIdAsync(Guid id)
    {
        var product = await _productRepository.GetByIdAsync(id);
        
        IsValid(product);
        
        return Response<GetProductDto>.Success(ObjectMapper.Mapper.Map<GetProductDto>(product),200);
    }

    public async Task<Response<List<GetProductDto>>> GetByCategory(Guid categoryId)
    {
        var products = await _productRepository.GetByCategory(categoryId);

        var productListDto = ObjectMapper.Mapper.Map<List<GetProductDto>>(products);
        
        return Response<List<GetProductDto>>.Success(productListDto,200);
    }

    private static void IsValid(Product? product)
    {
        IsNull(product);
    }

    private static void IsNull(Product? product)
    {
        if (product == null)
            throw new ProductNotFoundException("Product not found!");
    }
}