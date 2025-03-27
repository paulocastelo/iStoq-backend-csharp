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
        IEnumerable<CategoryReadDto> GetAll();
        CategoryReadDto? GetById(Guid id);
        CategoryReadDto Create(CategoryCreateDto dto);
        CategoryReadDto? Update(Guid id, CategoryUpdateDto dto);
        bool Delete(Guid id);
    }
}
