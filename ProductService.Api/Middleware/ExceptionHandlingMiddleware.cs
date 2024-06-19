using System.Net;

namespace ProductService.Api.Middleware
{

	public class ExceptionHandlingMiddleware
	{
		private readonly RequestDelegate _next;
		private readonly ILogger<ExceptionHandlingMiddleware> _logger;

		public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
		{
			_next = next;
			_logger = logger;
		}

		public async Task InvokeAsync(HttpContext context)
		{
			try
			{
				await _next(context);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "An unhandled exception occurred while processing the request.");
				await HandleExceptionAsync(context, ex);
			}
		}

		private static Task HandleExceptionAsync(HttpContext context, Exception exception)
		{
			context.Response.ContentType = "application/json";

			var response = new ErrorResponse();
			switch (exception)
			{
				case KeyNotFoundException _:
					context.Response.StatusCode = (int)HttpStatusCode.NotFound;
					response.ErrorCode = context.Response.StatusCode;
					response.Message = "No hay registros para la consulta";
					break;
				case ArgumentException _:
					context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
					response.ErrorCode = context.Response.StatusCode;
					response.Message = "Datos invalidos";
					break;
				default:
					context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
					response.ErrorCode = context.Response.StatusCode;
					response.Message = "Error interno. Consulta los logs para más detalle";
					break;
			}

			return context.Response.WriteAsync(System.Text.Json.JsonSerializer.Serialize(response));
		}

		private class ErrorResponse
		{
			public int ErrorCode { get; set; }
			public string? Message { get; set; }
		}
	}


}
