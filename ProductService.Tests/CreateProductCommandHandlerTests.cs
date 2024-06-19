using FluentAssertions;
using Moq;
using ProductService.Application.Features.Commands;
using ProductService.Application.Handlers.Commands;
using ProductService.Application.Interfaces;
using ProductService.Application.Response;
using ProductService.Domain.Entities;
namespace ProductService.Tests
{
	public class CreateProductCommandHandlerTests
	{
		private readonly Mock<IProductService> _productServiceMock;
		private readonly CreateProductCommandHandler _handler;

		public CreateProductCommandHandlerTests()
		{
			_productServiceMock = new Mock<IProductService>();

			_handler = new CreateProductCommandHandler(_productServiceMock.Object);
		}

		[Fact]
		public async Task Handle_ShouldAddProduct_WhenProductIsValid()
		{
			// Arrange
			var command = new CreateProductCommand
			{
				Name = "Test Product",
				Status = 1,
				Stock = 100,
				Description = "Test Description",
				Price = 10.99m
			};

			var productResponse = new ProductResponse
			{
				ProductId = 1,
				Name = "Test Product",
				StatusName = "Active",
				Stock = 100,
				Description = "Test Description",
				Price = 10.99m,
				Discount = 10,  
				FinalPrice = 9.89m 
			};

			_productServiceMock.Setup(service => service.CreateProduct(It.IsAny<Product>()))
				.ReturnsAsync(productResponse);

			// Act
			var result = await _handler.Handle(command, CancellationToken.None);

			// Assert
			result.Should().BeOfType<ProductResponse>();
			result.ProductId.Should().Be(productResponse.ProductId);
			result.Name.Should().Be(command.Name);
			result.StatusName.Should().Be("Active");
			result.Stock.Should().Be(command.Stock);
			result.Description.Should().Be(command.Description);
			result.Price.Should().Be(command.Price);
			result.Discount.Should().Be(productResponse.Discount);
			result.FinalPrice.Should().Be(productResponse.FinalPrice);
		}
	}
}
