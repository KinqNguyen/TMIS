
using Microsoft.AspNetCore.Http;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace TH_Project.BackendApi.Middlewares
{
    /// <summary>
    /// Config auth
    /// </summary>
    public class AuthMiddleware
    {
        private readonly RequestDelegate request;

        public AuthMiddleware(RequestDelegate RequestDelegate)
        {
            if (RequestDelegate == null) { throw new ArgumentNullException(nameof(RequestDelegate), nameof(RequestDelegate) + " is required"); }

            request = RequestDelegate;
        }

        /// <summary>
        /// Viết lại response 401
        /// </summary>
        /// <param name="Context"></param>
        /// <returns></returns>
        public async Task InvokeAsync(HttpContext Context)
        {
            if (Context == null) { throw new ArgumentNullException(nameof(Context), nameof(Context) + " is required"); }

            await request(Context);

            if (Context.Response.StatusCode == 401)
            {
                Context.Response.ContentType = "application/json";
                using (var writer = new Utf8JsonWriter(Context.Response.BodyWriter))
                {
                    writer.WriteStartObject();
                    writer.WriteNumber("code", 401);
                    writer.WriteString("desc", "Auth failed");
                    writer.WriteEndObject();
                    writer.Flush();
                }
            }
        }
    }
}
