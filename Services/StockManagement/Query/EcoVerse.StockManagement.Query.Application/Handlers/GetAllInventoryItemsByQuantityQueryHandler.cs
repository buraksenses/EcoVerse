using EcoVerse.Shared.DTOs;
using EcoVerse.StockManagement.Query.Application.DTOs;
using EcoVerse.StockManagement.Query.Application.Mappings;
using EcoVerse.StockManagement.Query.Application.Queries;
using EcoVerse.StockManagement.Query.Domain.Repositories;
using MediatR;

namespace EcoVerse.StockManagement.Query.Application.Handlers;

public class GetAllInventoryItemsByQuantityQueryHandler : IRequestHandler<GetAllInventoryItemsByQuantityQuery, Response<List<InventoryItemDto>>>
{
    private readonly IInventoryItemRepository _repository;

    public GetAllInventoryItemsByQuantityQueryHandler(IInventoryItemRepository repository)
    {
        _repository = repository;
    }
    
    public async Task<Response<List<InventoryItemDto>>> Handle(GetAllInventoryItemsByQuantityQuery request, CancellationToken cancellationToken)
    {
        var items = await _repository.GetAllByQuantity(request.Quantity);
        var itemListDto = ObjectMapper.Mapper.Map<List<InventoryItemDto>>(items);
        return Response<List<InventoryItemDto>>.Success(itemListDto,200);
    }
}