using EcoVerse.ProductManagement.Application.DTOs;
using EcoVerse.ProductManagement.Application.DTOs.Product;
using EcoVerse.ProductManagement.Application.Interfaces;
using EcoVerse.ProductManagement.Application.Mappings;
using EcoVerse.ProductManagement.Domain.Entities;
using EcoVerse.ProductManagement.Domain.Interfaces;
using EcoVerse.Shared.DTOs;

namespace EcoVerse.ProductManagement.Application.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;

    public ProductService(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }


    public async Task<Response<NoContent>> CreateAsync(CreateProductDto productDto)
    {
        var product = ObjectMapper.Mapper.Map<Product>(productDto);
        
        if(product == null)
            return Response<NoContent>.Fail("Product can not be null!", 400);

        await _productRepository.CreateAsync(product);
        
        return Response<NoContent>.Success(201);
    }

    public async Task<Response<List<GetProductDto>>> ListAllAsync()
    {
        var products = await _productRepository.ListAllAsync();

        var productListDto = ObjectMapper.Mapper.Map<List<GetProductDto>>(products);
        
        return Response<List<GetProductDto>>.Success(productListDto,200);
    }

    public async Task<Response<NoContent>> UpdateAsync(Guid id, UpdateProductDto productDto)
    {
        var existingProduct = await _productRepository.GetByIdAsync(id);
        
        if(existingProduct == null)
            return Response<NoContent>.Fail("Could not found product with given ID!",404);

        var newProduct = ObjectMapper.Mapper.Map<Product>(productDto);

        await _productRepository.UpdateAsync(newProduct);
        
        return Response<NoContent>.Success(204);
    }

    public async Task<Response<NoContent>> DeleteAsync(Guid id)
    {
        var existingProduct = await _productRepository.GetByIdAsync(id);
        
        if(existingProduct == null)
            return Response<NoContent>.Fail("Could not found product with given ID!",404);

        await _productRepository.DeleteAsync(existingProduct);
        
        return Response<NoContent>.Success(200);
    }

    public async Task<Response<GetProductDto>> GetByIdAsync(Guid id)
    {
        var product = await _productRepository.GetByIdAsync(id);
        
        return product == null 
            ? Response<GetProductDto>.Fail("Could not found product with given ID!",404) 
            : Response<GetProductDto>.Success(ObjectMapper.Mapper.Map<GetProductDto>(product),200);
    }
}