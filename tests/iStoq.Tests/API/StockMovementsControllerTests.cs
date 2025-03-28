using Microsoft.AspNetCore.Mvc;
using Moq;
using iStoq.API.Controllers;
using iStoq.Application.Interfaces;
using iStoq.Application.DTOs;
using Xunit;

namespace iStoq.Tests.API.Controllers
{
    public class StockMovementsControllerTests
    {
        private readonly Mock<IStockMovementService> _serviceMock;
        private readonly StockMovementsController _controller;

        public StockMovementsControllerTests()
        {
            _serviceMock = new Mock<IStockMovementService>();
            _controller = new StockMovementsController(_serviceMock.Object);
        }

        [Fact]
        public void GetAll_ShouldReturn200()
        {
            _serviceMock.Setup(s => s.GetAll()).Returns(new List<StockMovementDto>
            {
                new() { Id = Guid.NewGuid(), ProductId = Guid.NewGuid(), Quantity = 10, Type = "IN", Notes = "Teste" }
            });

            var result = _controller.GetAll();

            var ok = Assert.IsType<OkObjectResult>(result);
            var list = Assert.IsAssignableFrom<IEnumerable<StockMovementDto>>(ok.Value);
            Assert.Single(list);
        }

        [Fact]
        public void Create_ShouldReturn201()
        {
            var dto = new StockMovementDto
            {
                ProductId = Guid.NewGuid(),
                Quantity = 5,
                Type = "OUT",
                Notes = "Venda"
            };

            var output = new StockMovementDto
            {
                Id = Guid.NewGuid(),
                ProductId = dto.ProductId,
                Quantity = 5,
                Type = "OUT",
                Notes = "Venda",
                Date = DateTime.UtcNow
            };

            _serviceMock.Setup(s => s.Create(dto)).Returns(output);

            var result = _controller.Create(dto);

            var created = Assert.IsType<CreatedAtActionResult>(result);
            var value = Assert.IsType<StockMovementDto>(created.Value);
            Assert.Equal("OUT", value.Type);
        }

        [Fact]
        public void GetById_ShouldReturnOk_WhenExists()
        {
            var id = Guid.NewGuid();
            var movement = new StockMovementDto
            {
                Id = id,
                ProductId = Guid.NewGuid(),
                Quantity = 10,
                Type = "IN",
                Notes = "Teste"
            };
            _serviceMock.Setup(s => s.GetById(id)).Returns(movement);
            var result = _controller.GetById(id);
            var ok = Assert.IsType<OkObjectResult>(result);
            var value = Assert.IsType<StockMovementDto>(ok.Value);
            Assert.Equal("IN", value.Type);
        }

        [Fact]
        public void GetById_ShouldReturn404_WhenNotFound()
        {
            _serviceMock.Setup(s => s.GetById(It.IsAny<Guid>())).Returns((StockMovementDto?)null);
            var result = _controller.GetById(Guid.NewGuid());
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void Delete_ShouldReturn204_WhenSuccessful()
        {
            _serviceMock.Setup(s => s.Delete(It.IsAny<Guid>())).Returns(true);
            var result = _controller.Delete(Guid.NewGuid());
            Assert.IsType<NoContentResult>(result);
        }
    }
}