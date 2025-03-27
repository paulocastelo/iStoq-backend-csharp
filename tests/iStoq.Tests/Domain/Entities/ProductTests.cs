using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iStoq.Domain.Entities;

namespace iStoq.Tests.Domain.Entities;

public class ProductTests
{
    [Fact]
    public void Should_Create_Valid_Product()
    {
        var product = new Product("Mouse Gamer", "Mouse USB com LED", 150m, 20);

        Assert.Equal("Mouse Gamer", product.Name);
        Assert.Equal("Mouse USB com LED", product.Description);
        Assert.Equal(150m, product.Price);
        Assert.Equal(20, product.Stock);
    }

    [Fact]
    public void Should_Update_Product_Info()
    {
        var product = new Product("Caneta", "Azul", 2m, 100);
        product.Update("Caneta Preta", "Tinta preta", 2.5m);

        Assert.Equal("Caneta Preta", product.Name);
        Assert.Equal("Tinta preta", product.Description);
        Assert.Equal(2.5m, product.Price);
    }

    [Fact]
    public void Should_Add_And_Remove_Stock()
    {
        var product = new Product("Livro", "Ficção", 30m, 10);

        product.AddStock(5);
        Assert.Equal(15, product.Stock);

        product.RemoveStock(3);
        Assert.Equal(12, product.Stock);
    }
}
