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
        IEnumerable<SupplierDto> GetAll();
        SupplierDto? GetById(Guid id);
        SupplierDto Create(SupplierDto dto);
        SupplierDto? Update(Guid id, SupplierDto dto);
        bool Delete(Guid id);
    }
}
