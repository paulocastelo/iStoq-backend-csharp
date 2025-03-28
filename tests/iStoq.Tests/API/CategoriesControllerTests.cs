using Microsoft.AspNetCore.Mvc;
using Moq;
using iStoq.API.Controllers;
using iStoq.Application.Interfaces;
using iStoq.Application.DTOs;

namespace iStoq.Tests.API.Controllers;

public class CategoriesControllerTests
{
    private readonly Mock<ICategoryService> _serviceMock;
    private readonly CategoriesController _controller;

    public CategoriesControllerTests()
    {
        _serviceMock = new Mock<ICategoryService>();
        _controller = new CategoriesController(_serviceMock.Object);
    }

    [Fact]
    public void GetAll_ShouldReturn200_WithList()
    {
        _serviceMock.Setup(s => s.GetAll()).Returns(new List<CategoryDto>
        {
            new() { Id = Guid.NewGuid(), Name = "Categoria X", Description = "Teste" }
        });

        var result = _controller.GetAll();

        var ok = Assert.IsType<OkObjectResult>(result);
        var list = Assert.IsAssignableFrom<IEnumerable<CategoryDto>>(ok.Value);
        Assert.Single(list);
    }

    [Fact]
    public void Create_ShouldReturn201()
    {
        var input = new CategoryDto { Name = "Nova", Description = "Nova cat" };
        var output = new CategoryDto { Id = Guid.NewGuid(), Name = "Nova", Description = "Nova cat" };

        _serviceMock.Setup(s => s.Create(input)).Returns(output);

        var result = _controller.Create(input);

        var created = Assert.IsType<CreatedAtActionResult>(result);
        var value = Assert.IsType<CategoryDto>(created.Value);
        Assert.Equal("Nova", value.Name);
    }

    [Fact]
    public void GetById_ShouldReturnOk_WhenExists()
    {
        var id = Guid.NewGuid();
        var category = new CategoryDto { Id = id, Name = "Categoria X", Description = "Teste" };
        _serviceMock.Setup(s => s.GetById(id)).Returns(category);
        var result = _controller.GetById(id);
        var ok = Assert.IsType<OkObjectResult>(result);
        var value = Assert.IsType<CategoryDto>(ok.Value);
        Assert.Equal("Categoria X", value.Name);
    }
    [Fact]
    public void GetById_ShouldReturn404_WhenNotFound()
    {
        var id = Guid.NewGuid();
        _serviceMock.Setup(s => s.GetById(id)).Returns((CategoryDto?)null);
        var result = _controller.GetById(id);
        Assert.IsType<NotFoundResult>(result);
    }
    [Fact]
    public void Update_ShouldReturnOk_WhenSuccessful()
    {
        var id = Guid.NewGuid();
        var input = new CategoryDto { Name = "Nova", Description = "Nova cat" };
        var output = new CategoryDto { Id = id, Name = "Nova", Description = "Nova cat" };
        _serviceMock.Setup(s => s.Update(id, input)).Returns(output);
        var result = _controller.Update(id, input);
        var ok = Assert.IsType<OkObjectResult>(result);
        var value = Assert.IsType<CategoryDto>(ok.Value);
        Assert.Equal("Nova", value.Name);
    }

    [Fact]
    public void Delete_ShouldReturn204_WhenSuccessful()
    {
        var id = Guid.NewGuid();
        _serviceMock.Setup(s => s.Delete(id)).Returns(true);
        var result = _controller.Delete(id);
        Assert.IsType<NoContentResult>(result);
    }
}
