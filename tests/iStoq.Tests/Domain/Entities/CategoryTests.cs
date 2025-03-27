using iStoq.Domain.Entities;

namespace iStoq.Tests.Domain.Entities;

public class CategoryTests
{
    [Fact]
    public void Should_Create_Valid_Category()
    {
        var category = new Category("Eletrônicos", "Produtos eletrônicos diversos");

        Assert.Equal("Eletrônicos", category.Name);
        Assert.Equal("Produtos eletrônicos diversos", category.Description);
    }

    [Fact]
    public void Should_Update_Category()
    {
        var category = new Category("Velha", "Descrição velha");
        category.Update("Nova", "Nova descrição");

        Assert.Equal("Nova", category.Name);
        Assert.Equal("Nova descrição", category.Description);
    }
}
