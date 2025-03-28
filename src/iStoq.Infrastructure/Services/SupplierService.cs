using AutoMapper;
using iStoq.Application.DTOs;
using iStoq.Application.Interfaces;
using iStoq.Domain.Entities;

namespace iStoq.Infrastructure.Services
{
    public class SupplierService : ISupplierService
    {
        private readonly IMapper _mapper;
        private readonly List<Supplier> _suppliers = new();

        public SupplierService(IMapper mapper)
        {
            _mapper = mapper;
        }

        public IEnumerable<SupplierDto> GetAll() =>
            _suppliers.Select(s => _mapper.Map<SupplierDto>(s));

        public SupplierDto Create(SupplierDto dto)
        {
            var supplier = _mapper.Map<Supplier>(dto);
            _suppliers.Add(supplier);
            return _mapper.Map<SupplierDto>(supplier);
        }

        public SupplierDto? GetById(Guid id)
        {
            var supplier = _suppliers.FirstOrDefault(s => s.Id == id);
            return supplier is null ? null : _mapper.Map<SupplierDto>(supplier);
        }

        public SupplierDto? Update(Guid id, SupplierDto dto)
        {
            var supplier = _suppliers.FirstOrDefault(s => s.Id == id);
            if (supplier is null) return null;

            supplier.Update(dto.Name, dto.CNPJ, dto.Email, dto.Phone);
            return _mapper.Map<SupplierDto>(supplier);
        }

        public bool Delete(Guid id)
        {
            var supplier = _suppliers.FirstOrDefault(s => s.Id == id);
            if (supplier is null) return false;
            _suppliers.Remove(supplier);
            return true;
        }
    }
}
