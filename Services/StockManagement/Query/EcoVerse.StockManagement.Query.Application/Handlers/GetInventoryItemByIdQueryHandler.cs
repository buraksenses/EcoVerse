using EcoVerse.Shared.DTOs;
using EcoVerse.StockManagement.Query.Application.DTOs;
using EcoVerse.StockManagement.Query.Application.Mappings;
using EcoVerse.StockManagement.Query.Application.Queries;
using EcoVerse.StockManagement.Query.Domain.Repositories;
using MediatR;

namespace EcoVerse.StockManagement.Query.Application.Handlers;

public class GetInventoryItemByIdQueryHandler : IRequestHandler<GetInventoryItemByIdQuery, Response<InventoryItemDto>>
{
    private readonly IInventoryItemRepository _repository;

    public GetInventoryItemByIdQueryHandler(IInventoryItemRepository repository)
    {
        _repository = repository;
    }
    
    public async Task<Response<InventoryItemDto>> Handle(GetInventoryItemByIdQuery request, CancellationToken cancellationToken)
    {
        var item = await _repository.GetByIdAsync(request.Id);
        if (item == null)
            throw new Exception("Item not found!");
        var itemDto = ObjectMapper.Mapper.Map<InventoryItemDto>(item);
        return Response<InventoryItemDto>.Success(itemDto,200);
    }
}