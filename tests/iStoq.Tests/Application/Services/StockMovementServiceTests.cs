using AutoMapper;
using iStoq.Application.DTOs;
using iStoq.Infrastructure.Services;
using iStoq.Application.Mappings; // ou onde estiver o seu MappingProfile
using Xunit;

namespace iStoq.Tests.Application.Services;

public class StockMovementServiceTests
{
    private readonly IMapper _mapper;

    public StockMovementServiceTests()
    {
        var config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<MappingProfile>(); // Garante que o AutoMapper use o perfil certo
        });

        _mapper = config.CreateMapper();
    }

    [Fact]
    public void Should_Create_Stock_Movement()
    {
        var service = new StockMovementService(_mapper);

        var dto = new StockMovementDto
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
        var service = new StockMovementService(_mapper);

        service.Create(new StockMovementDto { ProductId = Guid.NewGuid(), Quantity = 5, Type = "IN", Notes = "Entrada" });
        service.Create(new StockMovementDto { ProductId = Guid.NewGuid(), Quantity = 3, Type = "OUT", Notes = "Venda" });

        var all = service.GetAll().ToList();

        Assert.Equal(2, all.Count);
    }
}
