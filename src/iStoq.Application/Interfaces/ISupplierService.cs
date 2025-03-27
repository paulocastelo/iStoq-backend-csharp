using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iStoq.Application.DTOs;

namespace iStoq.Application.Interfaces
{
    public interface ISupplierService
    {
        IEnumerable<SupplierReadDto> GetAll();
        SupplierReadDto? GetById(Guid id);
        SupplierReadDto Create(SupplierCreateDto dto);
        SupplierReadDto? Update(Guid id, SupplierUpdateDto dto);
        bool Delete(Guid id);
    }
}
