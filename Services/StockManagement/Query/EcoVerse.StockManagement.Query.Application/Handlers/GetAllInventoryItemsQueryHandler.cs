using EcoVerse.Shared.DTOs;
using EcoVerse.StockManagement.Query.Application.DTOs;
using EcoVerse.StockManagement.Query.Application.Mappings;
using EcoVerse.StockManagement.Query.Application.Queries;
using EcoVerse.StockManagement.Query.Domain.Repositories;
using MediatR;

namespace EcoVerse.StockManagement.Query.Application.Handlers;

public class GetAllInventoryItemsQueryHandler : IRequestHandler<GetAllInventoryItemsQuery, Response<List<InventoryItemDto>>>
{
    private readonly IInventoryItemRepository _repository;

    public GetAllInventoryItemsQueryHandler(IInventoryItemRepository repository)
    {
        _repository = repository;
    }
    
    public async Task<Response<List<InventoryItemDto>>> Handle(GetAllInventoryItemsQuery request, CancellationToken cancellationToken)
    {
        var items = await _repository.GetAllAsync();
        var itemListDto = ObjectMapper.Mapper.Map<List<InventoryItemDto>>(items);
        return Response<List<InventoryItemDto>>.Success(itemListDto,200);
    }
}