using EcoVerse.ProductManagement.Application.DTOs.Product;
using EcoVerse.ProductManagement.Application.Interfaces;
using EcoVerse.ProductManagement.Application.Mappings;
using EcoVerse.ProductManagement.Domain.Entities;
using EcoVerse.ProductManagement.Domain.Exceptions;
using EcoVerse.ProductManagement.Domain.Interfaces;
using EcoVerse.Shared.DTOs;
using EcoVerse.Shared.Messages;
using MassTransit;

namespace EcoVerse.ProductManagement.Application.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;
    private readonly IPublishEndpoint _publishEndpoint;

    public ProductService(IProductRepository productRepository, IPublishEndpoint publishEndpoint)
    {
        _productRepository = productRepository;
        _publishEndpoint = publishEndpoint;
    }

    public async Task<Shared.DTOs.Response<CreateProductDto>> CreateAsync(CreateProductDto productDto)
    {
        var product = ObjectMapper.Mapper.Map<Product>(productDto);
        
        IsValid(product);

        await _productRepository.CreateAsync(product);

        await _publishEndpoint.Publish<AddNewProductEvent>(new AddNewProductEvent
        {
            Description = product.Description,
            Name = product.Name,
            Price = product.Price,
            ProductId = product.Id,
            Quantity = productDto.Quantity
        });
        
        return Shared.DTOs.Response<CreateProductDto>.Success(productDto, 201);
    }

    public async Task<Shared.DTOs.Response<List<GetProductDto>>> ListAllAsync(string? filterOn = null, string? filterQuery = null,string?
            sortBy = null,bool? isAscending = null,
        int pageNumber = 1, int pageSize = 1000)
    {
        var products = await _productRepository.ListAllAsync(filterOn, filterQuery, sortBy, isAscending,
            pageNumber, pageSize);

        var productListDto = ObjectMapper.Mapper.Map<List<GetProductDto>>(products);
        
        return Shared.DTOs.Response<List<GetProductDto>>.Success(productListDto,200);
    }

    public async Task<Shared.DTOs.Response<NoContent>> UpdateAsync(Guid id, UpdateProductDto productDto)
    {
        var existingProduct = await _productRepository.GetByIdAsync(id);
        
        IsValid(existingProduct);

        ObjectMapper.Mapper.Map(productDto, existingProduct);

        await _productRepository.UpdateAsync(existingProduct);
        
        return Shared.DTOs.Response<NoContent>.Success(204);
    }

    public async Task<Shared.DTOs.Response<NoContent>> DeleteAsync(Guid id)
    {
        var existingProduct = await _productRepository.GetByIdAsync(id);
        
        IsValid(existingProduct);

        await _productRepository.DeleteAsync(existingProduct);
        
        return Shared.DTOs.Response<NoContent>.Success(200);
    }

    public async Task<Shared.DTOs.Response<GetProductDto>> GetByIdAsync(Guid id)
    {
        var product = await _productRepository.GetByIdAsync(id);
        
        IsValid(product);
        
        return Shared.DTOs.Response<GetProductDto>.Success(ObjectMapper.Mapper.Map<GetProductDto>(product),200);
    }

    public async Task<Shared.DTOs.Response<List<GetProductDto>>> GetByCategory(Guid categoryId)
    {
        var products = await _productRepository.GetByCategory(categoryId);

        var productListDto = ObjectMapper.Mapper.Map<List<GetProductDto>>(products);
        
        return Shared.DTOs.Response<List<GetProductDto>>.Success(productListDto,200);
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