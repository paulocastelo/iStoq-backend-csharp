using AutoMapper;
using iStoq.Application.DTOs;
using iStoq.Application.Interfaces;
using iStoq.Domain.Entities;

namespace iStoq.Infrastructure.Services;

public class ProductService : IProductService
{
    private readonly List<Product> _products = new();
    private readonly IMapper _mapper;

    public ProductService(IMapper mapper)
    {
        _mapper = mapper;
    }

    public IEnumerable<ProductDto> GetAll() =>
        _mapper.Map<IEnumerable<ProductDto>>(_products);

    public ProductDto? GetById(Guid id)
    {
        var product = _products.FirstOrDefault(p => p.Id == id);
        return product is null ? null : _mapper.Map<ProductDto>(product);
    }

    public ProductDto Create(ProductDto dto)
    {
        var product = new Product(dto.Name, dto.Description, dto.Price, dto.Stock);
        _products.Add(product);
        return _mapper.Map<ProductDto>(product);
    }

    public ProductDto? Update(Guid id, ProductDto dto)
    {
        var product = _products.FirstOrDefault(p => p.Id == id);
        if (product is null) return null;

        product.Update(dto.Name, dto.Description, dto.Price, dto.Stock);
        return _mapper.Map<ProductDto>(product);
    }

    public bool Delete(Guid id)
    {
        var product = _products.FirstOrDefault(p => p.Id == id);
        if (product is null) return false;

        _products.Remove(product);
        return true;
    }
}
