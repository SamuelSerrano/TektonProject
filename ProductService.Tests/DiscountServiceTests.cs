using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Moq;
using Moq.Protected;
using System.Net;

namespace ProductService.Tests
{
	public class DiscountServiceTests
	{
		private readonly Mock<HttpMessageHandler> _httpMessageHandlerMock;
		private readonly HttpClient _httpClient;
		private readonly IConfiguration _configuration;
		private readonly DiscountService _discountService;

		public DiscountServiceTests()
		{
			_httpMessageHandlerMock = new Mock<HttpMessageHandler>();
			_httpClient = new HttpClient(_httpMessageHandlerMock.Object);
			_configuration = new ConfigurationBuilder()
				.AddInMemoryCollection(new Dictionary<string, string>
				{
				{ "ExternalApiSettings:BaseUrl", "https://api.external-service.com/" },
				{ "ExternalApiSettings:ApiKey", "your_api_key" }
				})
				.Build();

			_discountService = new DiscountService(_httpClient, _configuration);
		}

		[Fact]
		public async Task GetDiscountAsync_ShouldReturnDiscount_WhenApiReturnsSuccess()
		{
			// Arrange
			var productId = 1;
			var responseContent = new StringContent("[{\"discount\": 10}]");
			_httpMessageHandlerMock.Protected()
				.Setup<Task<HttpResponseMessage>>(
					"SendAsync",
					ItExpr.IsAny<HttpRequestMessage>(),
					ItExpr.IsAny<CancellationToken>())
				.ReturnsAsync(new HttpResponseMessage
				{
					StatusCode = HttpStatusCode.OK,
					Content = responseContent
				});

			// Act
			var discount = await _discountService.GetDiscountAsync(productId);

			// Assert
			discount.Should().Be(10);
		}
	}
}
