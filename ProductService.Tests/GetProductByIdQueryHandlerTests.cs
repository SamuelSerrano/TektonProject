using FluentAssertions;
using Moq;
using ProductService.Application.Features.Queries;
using ProductService.Application.Handlers.Queries;
using ProductService.Application.Interfaces;
using ProductService.Application.Response;

namespace ProductService.Tests
{
	public class GetProductByIdQueryHandlerTests
	{
		private readonly Mock<IProductService> _productServiceMock;
		private readonly GetProductByIdQueryHandler _handler;

		public GetProductByIdQueryHandlerTests()
		{
			_productServiceMock = new Mock<IProductService>();
			_handler = new GetProductByIdQueryHandler(_productServiceMock.Object);
		}

		[Fact]
		public async Task Handle_ShouldReturnProductResponse_WhenProductExists()
		{
			// Arrange
			var query = new GetProductByIdQuery { ProductId = 1 };

			var productResponse = new ProductResponse
			{
				ProductId = 1,
				Name = "Test Product",
				StatusName = "Active",
				Stock = 100,
				Description = "Test Description",
				Price = 10.99m,
				Discount = 10,  // Example discount
				FinalPrice = 9.89m // Example final price after discount
			};

			_productServiceMock.Setup(service => service.GetProductById(It.IsAny<int>()))
				.ReturnsAsync(productResponse);

			// Act
			var result = await _handler.Handle(query, CancellationToken.None);

			// Assert
			result.Should().BeOfType<ProductResponse>();
			result.ProductId.Should().Be(productResponse.ProductId);
			result.Name.Should().Be(productResponse.Name);
			result.StatusName.Should().Be(productResponse.StatusName);
			result.Stock.Should().Be(productResponse.Stock);
			result.Description.Should().Be(productResponse.Description);
			result.Price.Should().Be(productResponse.Price);
			result.Discount.Should().Be(productResponse.Discount);
			result.FinalPrice.Should().Be(productResponse.FinalPrice);
		}
	}
}
