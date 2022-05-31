

using Building.API.Defined.Enums;
using Building.API.Types;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace TH_Project.BackendApi.Controllers.Middlewares
{
    public static class ResponseParserMiddlewareExtensions
    {
        public static IApplicationBuilder UseResponseParser(
            this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ResponseParserMiddleware>();
        }
    }

    public class ResponseParserMiddleware
    {
        private readonly RequestDelegate _next;

        public ResponseParserMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var originBody = context.Response.Body;
            try
            {
                var memStream = new MemoryStream();
                context.Response.Body = memStream;

                await _next(context).ConfigureAwait(false);

                memStream.Position = 0;
                var responseBody = new StreamReader(memStream).ReadToEnd();

                var statusCode = context.Response.StatusCode;
                var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
                if (statusCode == 404)
                {
                    context.Response.ContentType = "application/json";
                  
                    responseBody = JsonSerializer.Serialize(
                        Responses.Error(ResponseCodes.NotFound, "Not Found"),
                        options);
                }
                else
                {
                    if (responseBody.Length == 0)
                    {
                        context.Response.ContentType = "application/json";

                        responseBody = JsonSerializer.Serialize(
                            Responses.Success("Success"),
                            options);
                    }
                }

                var memoryStreamModified = new MemoryStream();
                var sw = new StreamWriter(memoryStreamModified);
                sw.Write(responseBody);
                sw.Flush();
                memoryStreamModified.Position = 0;

                await memoryStreamModified.CopyToAsync(originBody).ConfigureAwait(false);
            }
            finally
            {
                context.Response.Body = originBody;
            }
        }
    }
}
