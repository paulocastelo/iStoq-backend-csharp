using AutoMapper;
using iStoq.Application.DTOs;
using iStoq.Application.Interfaces;
using iStoq.Domain.Entities;

namespace iStoq.Infrastructure.Services;

public class StockMovementService : IStockMovementService
{
    private readonly List<StockMovement> _movements = new();
    private readonly IMapper _mapper;

    public StockMovementService(IMapper mapper)
    {
        _mapper = mapper;
    }

    public IEnumerable<StockMovementDto> GetAll() =>
        _mapper.Map<IEnumerable<StockMovementDto>>(_movements);

    public StockMovementDto? GetById(Guid id)
    {
        var movement = _movements.FirstOrDefault(m => m.Id == id);
        return movement is null ? null : _mapper.Map<StockMovementDto>(movement);
    }

    public StockMovementDto Create(StockMovementDto dto)
    {
        var movement = new StockMovement(dto.ProductId, dto.Quantity, dto.Type, dto.Notes);
        _movements.Add(movement);
        return _mapper.Map<StockMovementDto>(movement);
    }

    public StockMovementDto? Update(Guid id, StockMovementDto dto)
    {
        var movement = _movements.FirstOrDefault(m => m.Id == id);
        if (movement is null) return null;

        movement.Update(dto.ProductId, dto.Quantity, dto.Type, dto.Notes);
        return _mapper.Map<StockMovementDto>(movement);
    }

    public bool Delete(Guid id)
    {
        var movement = _movements.FirstOrDefault(m => m.Id == id);
        if (movement is null) return false;

        _movements.Remove(movement);
        return true;
    }
}
