using Microsoft.AspNetCore.Mvc;
using Moq;
using iStoq.API.Controllers;
using iStoq.Application.Interfaces;
using iStoq.Application.DTOs;

namespace iStoq.Tests.API.Controllers;

public class SuppliersControllerTests
{
    private readonly Mock<ISupplierService> _serviceMock;
    private readonly SuppliersController _controller;

    public SuppliersControllerTests()
    {
        _serviceMock = new Mock<ISupplierService>();
        _controller = new SuppliersController(_serviceMock.Object);
    }

    [Fact]
    public void GetAll_ShouldReturn200()
    {
        _serviceMock.Setup(s => s.GetAll()).Returns(new List<SupplierReadDto>
        {
            new() { Id = Guid.NewGuid(), Name = "Fornecedor 1", CNPJ = "123", Email = "a@a.com", Phone = "123" }
        });

        var result = _controller.GetAll();

        var ok = Assert.IsType<OkObjectResult>(result);
        var list = Assert.IsAssignableFrom<IEnumerable<SupplierReadDto>>(ok.Value);
        Assert.Single(list);
    }

    [Fact]
    public void Create_ShouldReturn201()
    {
        var input = new SupplierCreateDto
        {
            Name = "Fornecedor Novo",
            CNPJ = "123456",
            Email = "f@x.com",
            Phone = "9999"
        };

        var output = new SupplierReadDto
        {
            Id = Guid.NewGuid(),
            Name = "Fornecedor Novo",
            CNPJ = "123456",
            Email = "f@x.com",
            Phone = "9999"
        };

        _serviceMock.Setup(s => s.Create(input)).Returns(output);

        var result = _controller.Create(input);

        var created = Assert.IsType<CreatedAtActionResult>(result);
        var value = Assert.IsType<SupplierReadDto>(created.Value);
        Assert.Equal("Fornecedor Novo", value.Name);
    }
}
