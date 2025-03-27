using iStoq.Domain.Entities;

namespace iStoq.Tests.Domain.Entities;

public class StockMovementTests
{
    [Fact]
    public void Should_Create_Valid_Movement()
    {
        var productId = Guid.NewGuid();
        var movement = new StockMovement(productId, 10, "IN", "Entrada de teste");

        Assert.Equal(productId, movement.ProductId);
        Assert.Equal(10, movement.Quantity);
        Assert.Equal("IN", movement.Type);
        Assert.Equal("Entrada de teste", movement.Notes);
        Assert.True(movement.Date <= DateTime.UtcNow);
    }
}
