using AutoMapper;
using iStoq.Application.DTOs;
using iStoq.Infrastructure.Services;

namespace iStoq.Tests.Application.Services;

public class CategoryServiceTests
{
    private readonly IMapper _mapper;

    [Fact]
    public void Should_Create_Category_From_DTO()
    {
        var service = new CategoryService(_mapper);

        var dto = new CategoryDto
        {
            Name = "Informática",
            Description = "Produtos de tecnologia"
        };

        var created = service.Create(dto);

        Assert.NotNull(created);
        Assert.Equal("Informática", created.Name);
        Assert.Equal("Produtos de tecnologia", created.Description);
    }

    [Fact]
    public void Should_Return_All_Categories()
    {
        var service = new CategoryService(_mapper);

        service.Create(new CategoryDto { Name = "Livros", Description = "Categoria 1" });
        service.Create(new CategoryDto { Name = "Alimentos", Description = "Categoria 2" });

        var all = service.GetAll().ToList();

        Assert.Equal(2, all.Count);
    }
}
