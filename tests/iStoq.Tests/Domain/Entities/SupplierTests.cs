using iStoq.Domain.Entities;

namespace iStoq.Tests.Domain.Entities;

public class SupplierTests
{
    [Fact]
    public void Should_Create_Supplier_With_Valid_Data()
    {
        var supplier = new Supplier("ACME Ltda", "12.345.678/0001-90", "contato@acme.com", "(11) 99999-0000");

        Assert.Equal("ACME Ltda", supplier.Name);
        Assert.Equal("12.345.678/0001-90", supplier.CNPJ);
        Assert.Equal("contato@acme.com", supplier.Email);
        Assert.Equal("(11) 99999-0000", supplier.Phone);
    }

    [Fact]
    public void Should_Update_Supplier_Data()
    {
        var supplier = new Supplier("Old", "00.000.000/0000-00", "old@email.com", "1234");
        supplier.Update("New", "11.111.111/1111-11", "new@email.com", "5678");

        Assert.Equal("New", supplier.Name);
        Assert.Equal("11.111.111/1111-11", supplier.CNPJ);
        Assert.Equal("new@email.com", supplier.Email);
        Assert.Equal("5678", supplier.Phone);
    }
}
