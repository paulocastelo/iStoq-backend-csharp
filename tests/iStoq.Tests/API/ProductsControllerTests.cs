using Microsoft.AspNetCore.Mvc;
using Moq;
using iStoq.API.Controllers;
using iStoq.Application.Interfaces;
using iStoq.Application.DTOs;
using Xunit;

namespace iStoq.Tests.API.Controllers
{
    public class ProductsControllerTests
    {
        private readonly Mock<IProductService> _serviceMock;
        private readonly ProductsController _controller;

        public ProductsControllerTests()
        {
            _serviceMock = new Mock<IProductService>();
            _controller = new ProductsController(_serviceMock.Object);
        }

        [Fact]
        public void GetAll_ShouldReturn200_WithList()
        {
            _serviceMock.Setup(s => s.GetAll()).Returns(new List<ProductReadDto>
            {
                new() { Id = Guid.NewGuid(), Name = "Produto Teste", Description = "Desc", Price = 10, Stock = 1 }
            });

            var result = _controller.GetAll();

            var okResult = Assert.IsType<OkObjectResult>(result);
            var value = Assert.IsAssignableFrom<IEnumerable<ProductReadDto>>(okResult.Value);
            Assert.Single(value);
        }

        [Fact]
        public void Create_ShouldReturn201_AndReturnCreatedProduct()
        {
            var input = new ProductCreateDto
            {
                Name = "Produto Novo",
                Description = "Descrição",
                Price = 100,
                Stock = 5
            };

            var output = new ProductReadDto
            {
                Id = Guid.NewGuid(),
                Name = "Produto Novo",
                Description = "Descrição",
                Price = 100,
                Stock = 5
            };

            _serviceMock.Setup(s => s.Create(input)).Returns(output);

            var result = _controller.Create(input);

            var created = Assert.IsType<CreatedAtActionResult>(result);
            var value = Assert.IsType<ProductReadDto>(created.Value);
            Assert.Equal("Produto Novo", value.Name);
        }

        [Fact]
        public void GetById_ShouldReturn404_IfNotFound()
        {
            _serviceMock.Setup(s => s.GetById(It.IsAny<Guid>())).Returns((ProductReadDto?)null);

            var result = _controller.GetById(Guid.NewGuid());

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void GetById_ShouldReturnOk_WhenExists()
        {
            var id = Guid.NewGuid();
            var dto = new ProductReadDto { Id = id, Name = "Produto X", Description = "", Price = 10, Stock = 5 };

            _serviceMock.Setup(s => s.GetById(id)).Returns(dto);

            var result = _controller.GetById(id);

            var ok = Assert.IsType<OkObjectResult>(result);
            var value = Assert.IsType<ProductReadDto>(ok.Value);
            Assert.Equal("Produto X", value.Name);
        }

        [Fact]
        public void GetById_ShouldReturn404_WhenNotFound()
        {
            _serviceMock.Setup(s => s.GetById(It.IsAny<Guid>())).Returns((ProductReadDto?)null);

            var result = _controller.GetById(Guid.NewGuid());

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void Update_ShouldReturnOk_WhenSuccessful()
        {
            // Arrange
            var id = Guid.NewGuid();
            var input = new ProductUpdateDto
            {
                Name = "Produto Atualizado",
                Description = "Desc",
                Price = 100,
                Stock = 10
            };
            var output = new ProductReadDto
            {
                Id = id,
                Name = "Produto Atualizado",
                Description = "Desc",
                Price = 100,
                Stock = 10
            };
            _serviceMock.Setup(s => s.Update(id, input)).Returns(output);
            // Act
            var result = _controller.Update(id, input);
            // Assert
            var ok = Assert.IsType<OkObjectResult>(result);
            var value = Assert.IsType<ProductReadDto>(ok.Value);
            Assert.Equal("Produto Atualizado", value.Name);
        }

        [Fact]
        public void Update_ShouldReturn404_WhenNotFound()
        {
            // Arrange
            var id = Guid.NewGuid();
            var input = new ProductUpdateDto
            {
                Name = "Inexistente",
                Description = "Desc",
                Price = 100,
                Stock = 10
            };

            _serviceMock.Setup(s => s.Update(id, input)).Returns((ProductReadDto?)null);

            // Act
            var result = _controller.Update(id, input);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void Delete_ShouldReturn204_WhenSuccessful()
        {
            // Arrange
            var id = Guid.NewGuid();
            _serviceMock.Setup(s => s.Delete(id)).Returns(true);

            // Act
            var result = _controller.Delete(id);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public void Delete_ShouldReturn404_WhenNotFound()
        {
            // Arrange
            var id = Guid.NewGuid();
            _serviceMock.Setup(s => s.Delete(id)).Returns(false);

            // Act
            var result = _controller.Delete(id);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }
    }
}