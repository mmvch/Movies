using Microsoft.Extensions.Primitives;
using Microsoft.Net.Http.Headers;
using Movies.Domain.DTO;
using Movies.Domain.Exceptions;
using Movies.Domain.ExtensionMethods;


namespace Movies.Middleware
{
	public class ExceptionHandler(RequestDelegate requestDelegate)
	{
		private readonly RequestDelegate _requestDelegate = requestDelegate;

		public async Task InvokeAsync(HttpContext httpContext)
		{
			try
			{
				await _requestDelegate(httpContext);
			}
			catch (ServiceException exception)
			{
				await HandleServiceException(httpContext, exception);
			}
			catch (Exception exception)
			{
				await HandleException(httpContext, exception);
			}
		}

		private static async Task HandleError(HttpContext context, Error error)
		{
			string responseType = GetPreferredContentType(context.Request.Headers.Accept);

			context.Response.StatusCode = error.Status;
			context.Response.ContentType = responseType;

			string responseBody = responseType switch
			{
				"application/xml" => error.ToXml(),
				_ => error.ToJson(),
			};

			await context.Response.WriteAsync(responseBody);
		}

		private static string GetPreferredContentType(StringValues acceptHeader)
		{
			var mediaTypes = MediaTypeHeaderValue.ParseList(acceptHeader);

			var json = mediaTypes.FirstOrDefault(mt => mt.MediaType == "application/json");
			var xml = mediaTypes.FirstOrDefault(mt => mt.MediaType == "application/xml");

			double jsonQuality = json == null ? 0 : json.Quality ?? 1;
			double xmlQuality = xml == null ? 0 : xml.Quality ?? 1;

			return jsonQuality >= xmlQuality ? "application/json" : "application/xml";
		}

		private static async Task HandleServiceException(HttpContext context, ServiceException serviceException)
		{
			Error error = new()
			{
				Status = (int)serviceException.HttpStatusCode,
				Message = serviceException.Message
			};

			await HandleError(context, error);
		}

		private static async Task HandleException(HttpContext context, Exception exception)
		{
			await HandleError(context, new Error());

			// Log
			Console.WriteLine(exception.Message);
		}
	}
}
