using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iStoq.Application.DTOs;
using iStoq.Application.Interfaces;
using iStoq.Domain.Entities;

namespace iStoq.Infrastructure.Services;

public class ProductService : IProductService
{
    private readonly List<Product> _products = new();

    public IEnumerable<ProductReadDto> GetAll() =>
        _products.Select(p => new ProductReadDto
        {
            Id = p.Id,
            Name = p.Name,
            Description = p.Description,
            Price = p.Price,
            Stock = p.Stock
        });

    public ProductReadDto? GetById(Guid id)
    {
        var p = _products.FirstOrDefault(p => p.Id == id);
        return p is null ? null : new ProductReadDto
        {
            Id = p.Id,
            Name = p.Name,
            Description = p.Description,
            Price = p.Price,
            Stock = p.Stock
        };
    }

    public ProductReadDto Create(ProductCreateDto dto)
    {
        var product = new Product(dto.Name, dto.Description, dto.Price, dto.Stock);
        _products.Add(product);

        return new ProductReadDto
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            Price = product.Price,
            Stock = product.Stock
        };
    }

    public ProductReadDto? Update(Guid id, ProductUpdateDto dto)
    {
        var p = _products.FirstOrDefault(p => p.Id == id);
        if (p is null) return null;
        p.Update(dto.Name, dto.Description, dto.Price, dto.Stock);
        return new ProductReadDto
        {
            Id = p.Id,
            Name = p.Name,
            Description = p.Description,
            Price = p.Price,
            Stock = p.Stock
        };
    }

    public bool Delete(Guid id)
    {
        var p = _products.FirstOrDefault(p => p.Id == id);
        if (p is null) return false;
        _products.Remove(p);
        return true;
    }
}
