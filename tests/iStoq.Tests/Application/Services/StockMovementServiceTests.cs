using iStoq.Application.DTOs;
using iStoq.Infrastructure.Services;

namespace iStoq.Tests.Application.Services;

public class StockMovementServiceTests
{
    [Fact]
    public void Should_Create_Stock_Movement()
    {
        var service = new StockMovementService();

        var dto = new StockMovementCreateDto
        {
            ProductId = Guid.NewGuid(),
            Quantity = 5,
            Type = "IN",
            Notes = "Reposição"
        };

        var created = service.Create(dto);

        Assert.NotNull(created);
        Assert.Equal(5, created.Quantity);
        Assert.Equal("IN", created.Type);
        Assert.Equal("Reposição", created.Notes);
    }

    [Fact]
    public void Should_Return_All_Movements()
    {
        var service = new StockMovementService();

        service.Create(new StockMovementCreateDto { ProductId = Guid.NewGuid(), Quantity = 5, Type = "IN", Notes = "Entrada" });
        service.Create(new StockMovementCreateDto { ProductId = Guid.NewGuid(), Quantity = 3, Type = "OUT", Notes = "Venda" });

        var all = service.GetAll().ToList();

        Assert.Equal(2, all.Count);
    }
}
