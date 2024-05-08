using EcoVerse.Shared.DTOs;
using EcoVerse.StockManagement.Query.Application.DTOs;
using EcoVerse.StockManagement.Query.Application.Mappings;
using EcoVerse.StockManagement.Query.Application.Queries;
using EcoVerse.StockManagement.Query.Domain.Repositories;
using MediatR;

namespace EcoVerse.StockManagement.Query.Application.Handlers;

public class GetAllInventoryItemsByPriceQueryHandler : IRequestHandler<GetAllInventoryItemsByPriceQuery, Response<List<InventoryItemDto>>>
{
    private readonly IInventoryItemRepository _repository;

    public GetAllInventoryItemsByPriceQueryHandler(IInventoryItemRepository repository)
    {
        _repository = repository;
    }
    
    public async Task<Response<List<InventoryItemDto>>> Handle(GetAllInventoryItemsByPriceQuery request, CancellationToken cancellationToken)
    {
        var items = await _repository.GetAllByPrice(request.Price);
        var itemListDto = ObjectMapper.Mapper.Map<List<InventoryItemDto>>(items);
        return Response<List<InventoryItemDto>>.Success(itemListDto,200);
    }
}