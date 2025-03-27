using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iStoq.Application.DTOs;

namespace iStoq.Application.Interfaces;

public interface IProductService
{
    IEnumerable<ProductReadDto> GetAll();
    ProductReadDto? GetById(Guid id);
    ProductReadDto Create(ProductCreateDto dto);
    ProductReadDto? Update(Guid id, ProductUpdateDto dto);
    bool Delete(Guid id);
}

