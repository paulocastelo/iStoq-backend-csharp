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
    public class CategoryService : ICategoryService
    {
        private readonly List<Category> _categories = new();

        public IEnumerable<CategoryReadDto> GetAll() =>
            _categories.Select(c => new CategoryReadDto
            {
                Id = c.Id,
                Name = c.Name,
                Description = c.Description
            });

        public CategoryReadDto Create(CategoryCreateDto dto)
        {
            var category = new Category(dto.Name, dto.Description);
            _categories.Add(category);

            return new CategoryReadDto
            {
                Id = category.Id,
                Name = category.Name,
                Description = category.Description
            };
        }

        public CategoryReadDto? GetById(Guid id)
        {
            var c = _categories.FirstOrDefault(c => c.Id == id);
            return c is null ? null : new CategoryReadDto
            {
                Id = c.Id,
                Name = c.Name,
                Description = c.Description
            };
        }

        public CategoryReadDto? Update(Guid id, CategoryUpdateDto dto)
        {
            var c = _categories.FirstOrDefault(c => c.Id == id);
            if (c is null) return null;
            c.Update(dto.Name, dto.Description);
            return new CategoryReadDto
            {
                Id = c.Id,
                Name = c.Name,
                Description = c.Description
            };
        }

        public bool Delete(Guid id)
        {
            var c = _categories.FirstOrDefault(c => c.Id == id);
            if (c is null) return false;
            _categories.Remove(c);
            return true;
        }

    }
}
