using Building.API.Defined.Enums;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;

namespace Stump.Api.Configurations
{
    /// <summary>
    /// Viết lại error response
    /// </summary>
    public static class ResponseConfig
    {
        public static void ConfigureErrorResponse(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(c => c.Run(async context =>
            {
                var exception = context.Features
                    .Get<IExceptionHandlerPathFeature>()
                    .Error;

                ResponseCodes code = ResponseCodes.InternalServerError;
                string desc = exception.Message;

                await context.Response.WriteAsJsonAsync(new
                {
                    Code = code,
                    Desc = desc
                });
            }));
        }
    }
}
