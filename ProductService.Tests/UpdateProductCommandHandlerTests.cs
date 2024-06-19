using FluentAssertions;
using Moq;
using ProductService.Application.Features.Commands;
using ProductService.Application.Handlers.Commands;
using ProductService.Application.Interfaces;
using ProductService.Application.Response;
using ProductService.Domain.Entities;

namespace ProductService.Tests
{
	public class UpdateProductCommandHandlerTests
	{
		private readonly Mock<IProductService> _productServiceMock;
		private readonly UpdateProductCommandHandler _handler;

		public UpdateProductCommandHandlerTests()
		{
			_productServiceMock = new Mock<IProductService>();
			_handler = new UpdateProductCommandHandler(_productServiceMock.Object);
		}

		[Fact]
		public async Task Handle_ShouldUpdateProduct_WhenProductExists()
		{
			// Arrange
			var command = new UpdateProductCommand
			{
				ProductId = 1,
				Name = "Updated Product",
				Status = 1,
				Stock = 200,
				Description = "Updated Description",
				Price = 20.99m
			};

			var updatedProductResponse = new ProductResponse
			{
				ProductId = 1,
				Name = "Updated Product",
				StatusName = "Active",
				Stock = 200,
				Description = "Updated Description",
				Price = 20.99m,
				Discount = 10, // Example discount
				FinalPrice = 18.89m // Example final price after discount
			};

			_productServiceMock.Setup(service => service.UpdateProduct(command.ProductId, It.IsAny<Product>()))
				.ReturnsAsync(updatedProductResponse);

			// Act
			var result = await _handler.Handle(command, CancellationToken.None);

			// Assert
			result.Should().BeOfType<ProductResponse>();
			result.ProductId.Should().Be(command.ProductId);
			result.Name.Should().Be(command.Name);
			result.StatusName.Should().Be("Active");
			result.Stock.Should().Be(command.Stock);
			result.Description.Should().Be(command.Description);
			result.Price.Should().Be(command.Price);
			result.Discount.Should().Be(updatedProductResponse.Discount);
			result.FinalPrice.Should().Be(updatedProductResponse.FinalPrice);
		}
	}
}