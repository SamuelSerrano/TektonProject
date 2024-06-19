using FluentAssertions;
using Moq;
using ProductService.Application.Interfaces;
using ProductService.Domain.Entities;
using ProductService.Infrastructure.Repositories.Interfaces;
using System.Linq.Expressions;

namespace ProductService.Tests
{
	public class ProductServiceTests
	{
		private readonly IProductService _productService;
		private readonly Mock<IUnitOfWork> _unitOfWorkMock;
		private readonly Mock<IDiscountService> _discountServiceMock;
		private readonly Mock<ICacheService> _cacheServiceMock;

		public ProductServiceTests()
		{
			_unitOfWorkMock = new Mock<IUnitOfWork>();
			_discountServiceMock = new Mock<IDiscountService>();
			_cacheServiceMock = new Mock<ICacheService>();

			_productService = new Application.Services.ProductService(_unitOfWorkMock.Object, _cacheServiceMock.Object, _discountServiceMock.Object);
		}

		[Fact]
		public async Task CreateProduct_ShouldAddProduct()
		{
			// Arrange
			var product = new Product
			{
				ProductId = 1,
				Name = "Test Product",
				Status = 1,
				Stock = 10,
				Description = "Test Description",
				Price = 100
			};

			_unitOfWorkMock.Setup(u => u.Product.AddAsync(product)).Returns(Task.CompletedTask);
			_unitOfWorkMock.Setup(u => u.SaveAsync()).Returns(Task.CompletedTask);
			_cacheServiceMock.Setup(c => c.GetStatusName(product.Status)).Returns("Active");
			_discountServiceMock.Setup(d => d.GetDiscountAsync(product.ProductId)).ReturnsAsync(10);

			// Act
			var result = await _productService.CreateProduct(product);

			// Assert
			_unitOfWorkMock.Verify(u => u.Product.AddAsync(product), Times.Once);
			_unitOfWorkMock.Verify(u => u.SaveAsync(), Times.Once);
			result.Should().NotBeNull();
			result.ProductId.Should().Be(product.ProductId);
			result.Name.Should().Be(product.Name);
			result.Status.Should().Be(product.Status);
			result.Stock.Should().Be(product.Stock);
			result.Description.Should().Be(product.Description);
			result.Price.Should().Be(product.Price);
			result.Discount.Should().Be(10);
			result.FinalPrice.Should().Be(90); 
		}

		[Fact]
		public async Task UpdateProduct_ShouldModifyProduct()
		{
			// Arrange
			var productId = 1;
			var existingProduct = new Product
			{
				ProductId = productId,
				Name = "Existing Product",
				Status = 1,
				Stock = 10,
				Description = "Existing Description",
				Price = 100
			};

			var updateRequest = new Product
			{
				ProductId = productId,
				Name = "Updated Product",
				Status = 1,
				Stock = 20,
				Description = "Updated Description",
				Price = 200
			};

			_unitOfWorkMock.Setup(u => u.Product.GetAsync(x => x.ProductId == productId)).ReturnsAsync(existingProduct);
			_unitOfWorkMock.Setup(u => u.Product.UpdateAsync(It.IsAny<Product>())).Returns(Task.CompletedTask);
			_unitOfWorkMock.Setup(u => u.SaveAsync()).Returns(Task.CompletedTask);
			_cacheServiceMock.Setup(c => c.GetStatusName(existingProduct.Status)).Returns("Active");
			_discountServiceMock.Setup(d => d.GetDiscountAsync(existingProduct.ProductId)).ReturnsAsync(10);

			// Act
			var result = await _productService.UpdateProduct(productId, updateRequest);

			// Assert
			_unitOfWorkMock.Verify(u => u.Product.GetAsync(It.IsAny<Expression<Func<Product, bool>>>()), Times.Once);
			_unitOfWorkMock.Verify(u => u.Product.UpdateAsync(It.IsAny<Product>()), Times.Once);
			_unitOfWorkMock.Verify(u => u.SaveAsync(), Times.Once);

			result.Should().NotBeNull();
			result.ProductId.Should().Be(updateRequest.ProductId);
			result.Name.Should().Be(updateRequest.Name);
			result.Status.Should().Be(updateRequest.Status);
			result.Stock.Should().Be(updateRequest.Stock);
			result.Description.Should().Be(updateRequest.Description);
			result.Price.Should().Be(updateRequest.Price);
			result.Discount.Should().Be(10);
			result.FinalPrice.Should().Be(180);
		}


		[Fact]
		public async Task GetProductById_ShouldReturnProduct()
		{
			// Arrange
			var productId = 1;
			var product = new Product
			{
				ProductId = productId,
				Name = "Test Product",
				Status = 1,
				Stock = 10,
				Description = "Test Description",
				Price = 100
			};

			_unitOfWorkMock.Setup(u => u.Product.GetAsync(x => x.ProductId == productId)).ReturnsAsync(product);
			_cacheServiceMock.Setup(c => c.GetStatusName(product.Status)).Returns("Active");
			_discountServiceMock.Setup(d => d.GetDiscountAsync(product.ProductId)).ReturnsAsync(10);

			// Act
			var result = await _productService.GetProductById(productId);

			// Assert
			_unitOfWorkMock.Verify(u => u.Product.GetAsync(It.IsAny<Expression<Func<Product, bool>>>()), Times.Once);
			result.Should().NotBeNull();
			result.ProductId.Should().Be(product.ProductId);
			result.Name.Should().Be(product.Name);
			result.Status.Should().Be(product.Status);
			result.StatusName.Should().Be("Active");
			result.Stock.Should().Be(product.Stock);
			result.Description.Should().Be(product.Description);
			result.Price.Should().Be(product.Price);
			result.Discount.Should().Be(10);
			result.FinalPrice.Should().Be(90); 
		}
	}
}
