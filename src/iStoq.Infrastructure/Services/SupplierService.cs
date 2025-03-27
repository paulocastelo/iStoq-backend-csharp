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
    public class SupplierService : ISupplierService
    {
        private readonly List<Supplier> _suppliers = new();
        public IEnumerable<SupplierReadDto> GetAll() =>
            _suppliers.Select(s => new SupplierReadDto
            {
                Id = s.Id,
                Name = s.Name,
                CNPJ = s.CNPJ,
                Email = s.Email,
                Phone = s.Phone
            });
        public SupplierReadDto Create(SupplierCreateDto dto)
        {
            var supplier = new Supplier(dto.Name, dto.CNPJ, dto.Email, dto.Phone);
            _suppliers.Add(supplier);
            return new SupplierReadDto
            {
                Id = supplier.Id,
                Name = supplier.Name,
                CNPJ = supplier.CNPJ,
                Email = supplier.Email,
                Phone = supplier.Phone
            };
        }

        public SupplierReadDto? GetById(Guid id)
        {
            var s = _suppliers.FirstOrDefault(s => s.Id == id);
            return s is null ? null : new SupplierReadDto
            {
                Id = s.Id,
                Name = s.Name,
                CNPJ = s.CNPJ,
                Email = s.Email,
                Phone = s.Phone
            };
        }

        public SupplierReadDto? Update(Guid id, SupplierUpdateDto dto)
        {
            var s = _suppliers.FirstOrDefault(s => s.Id == id);
            if (s is null) return null;
            s.Update(dto.Name, dto.CNPJ, dto.Email, dto.Phone);
            return new SupplierReadDto
            {
                Id = s.Id,
                Name = s.Name,
                CNPJ = s.CNPJ,
                Email = s.Email,
                Phone = s.Phone
            };
        }

        public bool Delete(Guid id)
        {
            var s = _suppliers.FirstOrDefault(s => s.Id == id);
            if (s is null) return false;
            _suppliers.Remove(s);
            return true;
        }
    }
}
