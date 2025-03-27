using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iStoq.Application.DTOs;
using iStoq.Application.Interfaces;
using iStoq.Domain.Entities;

namespace iStoq.Infrastructure.Services
{
    public class StockMovementService : IStockMovementService
    {
        private readonly List<StockMovement> _movements = new();

        public IEnumerable<StockMovementReadDto> GetAll() =>
            _movements.Select(m => new StockMovementReadDto
            {
                Id = m.Id,
                ProductId = m.ProductId,
                Quantity = m.Quantity,
                Type = m.Type,
                Notes = m.Notes,
                Date = m.Date
            });

        public StockMovementReadDto? GetById(Guid id)
        {
            var movement = _movements.FirstOrDefault(m => m.Id == id);
            if (movement == null)
                return null;
            return new StockMovementReadDto
            {
                Id = movement.Id,
                ProductId = movement.ProductId,
                Quantity = movement.Quantity,
                Type = movement.Type,
                Notes = movement.Notes,
                Date = movement.Date
            };
        }

        public StockMovementReadDto Create(StockMovementCreateDto dto)
        {
            var movement = new StockMovement(dto.ProductId, dto.Quantity, dto.Type, dto.Notes);
            _movements.Add(movement);

            return new StockMovementReadDto
            {
                Id = movement.Id,
                ProductId = movement.ProductId,
                Quantity = movement.Quantity,
                Type = movement.Type,
                Notes = movement.Notes,
                Date = movement.Date
            };
        }

        public StockMovementReadDto? Update(Guid id, StockMovementUpdateDto dto)
        {
            var movement = _movements.FirstOrDefault(m => m.Id == id);
            if (movement == null)
                return null;
            movement.Update(dto.ProductId, dto.Quantity, dto.Type, dto.Notes);
            return new StockMovementReadDto
            {
                Id = movement.Id,
                ProductId = movement.ProductId,
                Quantity = movement.Quantity,
                Type = movement.Type,
                Notes = movement.Notes,
                Date = movement.Date
            };
        }

        public bool Delete(Guid id)
        {
            var movement = _movements.FirstOrDefault(m => m.Id == id);
            if (movement == null)
                return false;
            _movements.Remove(movement);
            return true;
        }
    }

}
