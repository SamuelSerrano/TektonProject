using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Moq;
namespace ProductService.Tests
{
	public class RequestLoggingMiddlewareTests
	{
		private readonly RequestLoggingMiddleware _middleware;
		private readonly Mock<RequestDelegate> _nextMock;
		private readonly Mock<ILogger<RequestLoggingMiddleware>> _loggerMock;

		public RequestLoggingMiddlewareTests()
		{
			_nextMock = new Mock<RequestDelegate>();
			_loggerMock = new Mock<ILogger<RequestLoggingMiddleware>>();
			_middleware = new RequestLoggingMiddleware(_nextMock.Object,_loggerMock.Object);
		}

		[Fact]
		public async Task InvokeAsync_ShouldLogResponseTime()
		{
			// Arrange
			var context = new DefaultHttpContext();
			var memoryStream = new MemoryStream();
			context.Response.Body = memoryStream;

			_nextMock.Setup(next => next(It.IsAny<HttpContext>()))
				.Returns(Task.CompletedTask);

			// Act
			await _middleware.InvokeAsync(context);

			// Assert
			var logFilePath = "logs/request_logs.txt";
			var logContents = await ReadLogFromFile(logFilePath);

			// Verify log contains expected message format
			Assert.Contains($"HTTP {context.Request.Method} {context.Request.Path} responded in", logContents);
		}

		private async Task<string> ReadLogFromFile(string logFilePath)
		{
			using (var reader = new StreamReader(logFilePath))
			{
				return await reader.ReadToEndAsync();
			}
		}
	}

}