using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iStoq.Application.DTOs;
using iStoq.Infrastructure.Services;

namespace iStoq.Tests.Application.Services;

public class ProductServiceTests
{
    [Fact]
    public void Should_Create_Product_From_DTO()
    {
        var service = new ProductService();

        var dto = new ProductCreateDto
        {
            Name = "Notebook",
            Description = "i5, 8GB RAM",
            Price = 3200.0m,
            Stock = 5
        };

        var result = service.Create(dto);

        Assert.NotNull(result);
        Assert.Equal("Notebook", result.Name);
        Assert.Equal("i5, 8GB RAM", result.Description);
        Assert.Equal(3200.0m, result.Price);
        Assert.Equal(5, result.Stock);
    }

    [Fact]
    public void Should_Return_All_Products()
    {
        var service = new ProductService();

        service.Create(new ProductCreateDto { Name = "Teclado", Description = "", Price = 100, Stock = 10 });
        service.Create(new ProductCreateDto { Name = "Monitor", Description = "", Price = 800, Stock = 3 });

        var all = service.GetAll().ToList();

        Assert.Equal(2, all.Count);
    }
}