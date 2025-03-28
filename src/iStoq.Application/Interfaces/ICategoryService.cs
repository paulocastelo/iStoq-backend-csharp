using iStoq.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iStoq.Application.Interfaces
{
    public interface ICategoryService
    {
        IEnumerable<CategoryDto> GetAll();
        CategoryDto? GetById(Guid id);
        CategoryDto Create(CategoryDto dto);
        CategoryDto? Update(Guid id, CategoryDto dto);
        bool Delete(Guid id);
    }

}
