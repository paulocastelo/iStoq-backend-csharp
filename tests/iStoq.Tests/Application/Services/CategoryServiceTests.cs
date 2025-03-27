using iStoq.Application.DTOs;
using iStoq.Infrastructure.Services;

namespace iStoq.Tests.Application.Services;

public class CategoryServiceTests
{
    [Fact]
    public void Should_Create_Category_From_DTO()
    {
        var service = new CategoryService();

        var dto = new CategoryCreateDto
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
        var service = new CategoryService();

        service.Create(new CategoryCreateDto { Name = "Livros", Description = "Categoria 1" });
        service.Create(new CategoryCreateDto { Name = "Alimentos", Description = "Categoria 2" });

        var all = service.GetAll().ToList();

        Assert.Equal(2, all.Count);
    }
}
