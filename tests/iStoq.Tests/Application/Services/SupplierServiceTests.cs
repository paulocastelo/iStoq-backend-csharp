using iStoq.Application.DTOs;
using iStoq.Infrastructure.Services;

namespace iStoq.Tests.Application.Services;

public class SupplierServiceTests
{
    [Fact]
    public void Should_Create_Supplier_From_DTO()
    {
        var service = new SupplierService();

        var dto = new SupplierCreateDto
        {
            Name = "Fornecedor X",
            CNPJ = "11.222.333/0001-44",
            Email = "contato@fornecedorx.com",
            Phone = "(11) 99999-0000"
        };

        var created = service.Create(dto);

        Assert.NotNull(created);
        Assert.Equal("Fornecedor X", created.Name);
        Assert.Equal("11.222.333/0001-44", created.CNPJ);
    }

    [Fact]
    public void Should_Return_All_Suppliers()
    {
        var service = new SupplierService();

        service.Create(new SupplierCreateDto { Name = "F1", CNPJ = "1", Email = "1", Phone = "1" });
        service.Create(new SupplierCreateDto { Name = "F2", CNPJ = "2", Email = "2", Phone = "2" });

        var all = service.GetAll().ToList();

        Assert.Equal(2, all.Count);
    }
}
