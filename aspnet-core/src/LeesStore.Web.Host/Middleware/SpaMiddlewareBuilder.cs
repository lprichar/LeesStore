using Microsoft.AspNetCore.Builder;
using System.IO;

namespace LeesStore.Web.Host.Middleware
{
    public static class SpaMiddlewareBuilder
    {
        public static void UseSpa(this IApplicationBuilder app)
        {
            app.Use(async (context, next) =>
            {
                await next();
                if (context.Response.StatusCode == 404 &&
                    context.Request.Path.StartsWithSegments("/app") &&
                    !Path.HasExtension(context.Request.Path.Value))
                {
                    context.Request.Path = "/index.html";
                    await next();
                }
            });
        }
    }
}
