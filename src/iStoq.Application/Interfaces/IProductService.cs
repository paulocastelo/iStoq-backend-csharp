using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iStoq.Application.DTOs;

namespace iStoq.Application.Interfaces;

public interface IProductService
{
    IEnumerable<ProductDto> GetAll();
    ProductDto? GetById(Guid id);
    ProductDto Create(ProductDto dto);
    ProductDto? Update(Guid id, ProductDto dto);
    bool Delete(Guid id);
}

