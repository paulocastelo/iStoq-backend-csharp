using AutoMapper;
using iStoq.Application.DTOs;
using iStoq.Application.Interfaces;
using iStoq.Domain.Entities;

namespace iStoq.Infrastructure.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly List<Category> _categories = new();
        private readonly IMapper _mapper;

        public CategoryService(IMapper mapper)
        {
            _mapper = mapper;
        }

        public IEnumerable<CategoryDto> GetAll() =>
            _mapper.Map<IEnumerable<CategoryDto>>(_categories);

        public CategoryDto Create(CategoryDto dto)
        {
            var category = new Category(dto.Name, dto.Description);
            _categories.Add(category);

            return _mapper.Map<CategoryDto>(category);
        }

        public CategoryDto? GetById(Guid id)
        {
            var category = _categories.FirstOrDefault(c => c.Id == id);
            return category is null ? null : _mapper.Map<CategoryDto>(category);
        }

        public CategoryDto? Update(Guid id, CategoryDto dto)
        {
            var category = _categories.FirstOrDefault(c => c.Id == id);
            if (category is null) return null;

            category.Update(dto.Name, dto.Description);
            return _mapper.Map<CategoryDto>(category);
        }

        public bool Delete(Guid id)
        {
            var category = _categories.FirstOrDefault(c => c.Id == id);
            if (category is null) return false;

            _categories.Remove(category);
            return true;
        }

    }
}
