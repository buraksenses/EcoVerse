using EcoVerse.Shared.DTOs;
using EcoVerse.StockManagement.Query.Application.DTOs;
using EcoVerse.StockManagement.Query.Application.Mappings;
using EcoVerse.StockManagement.Query.Application.Queries;
using EcoVerse.StockManagement.Query.Domain.Repositories;
using MediatR;

namespace EcoVerse.StockManagement.Query.Application.Handlers;

public class GetInventoryItemByProductIdQueryHandler : IRequestHandler<GetInventoryItemByProductIdQuery,Response<InventoryItemDto>>
{
    private readonly IInventoryItemRepository _repository;

    public GetInventoryItemByProductIdQueryHandler(IInventoryItemRepository repository)
    {
        _repository = repository;
    }
    
    public async Task<Response<InventoryItemDto>> Handle(GetInventoryItemByProductIdQuery request, CancellationToken cancellationToken)
    {
        var item = await _repository.GetByProductIdAsync(request.ProductId);
        var itemDto = ObjectMapper.Mapper.Map<InventoryItemDto>(item);
        return Response<InventoryItemDto>.Success(itemDto,200);
    }
}