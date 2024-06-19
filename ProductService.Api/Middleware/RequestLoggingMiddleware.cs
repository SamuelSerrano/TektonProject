using ProductServices.Utils;
using System.Diagnostics;

public class RequestLoggingMiddleware
{
	private readonly RequestDelegate _next;
	private readonly ILogger<RequestLoggingMiddleware> _logger;
	public RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger)
	{
		_next = next;
		_logger = logger;
	}

	public async Task InvokeAsync(HttpContext context)
	{
		var stopwatch = Stopwatch.StartNew();
		await _next(context);
		stopwatch.Stop();

		var logMessage = $"[{DateTime.Now}] HTTP {context.Request.Method} {context.Request.Path} responded in {stopwatch.ElapsedMilliseconds} ms";
		await FileHelper.LogToFileAsync(logMessage);
		_logger.LogInformation(logMessage);
	}

	
}
