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
        IEnumerable<StockMovementDto> GetAll();
        StockMovementDto? GetById(Guid id);
        StockMovementDto Create(StockMovementDto dto);
        StockMovementDto? Update(Guid id, StockMovementDto dto);
        bool Delete(Guid id);
    }
}
