using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iStoq.Application.DTOs;

namespace iStoq.Application.Interfaces
{
    public interface IStockMovementService
    {
        IEnumerable<StockMovementReadDto> GetAll();
        StockMovementReadDto? GetById(Guid id);
        StockMovementReadDto Create(StockMovementCreateDto dto);
        StockMovementReadDto? Update(Guid id, StockMovementUpdateDto dto);
        bool Delete(Guid id);
    }
}
