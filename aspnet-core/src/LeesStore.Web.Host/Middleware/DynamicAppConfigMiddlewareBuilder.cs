using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;

namespace LeesStore.Web.Host.Middleware
{
    public class DynamicAppConfigMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly string _serverRootAddress;
        private readonly string _clientRootAddress;

        public DynamicAppConfigMiddleware(RequestDelegate next, string serverRootAddress, string clientRootAddress)
        {
            _next = next;
            _serverRootAddress = serverRootAddress;
            _clientRootAddress = clientRootAddress;
        }

        public async Task Invoke(HttpContext context)
        {
            const string appConfigPath = "/assets/appconfig.production.json";
            var isRequestingAppConfig = appConfigPath.Equals(context.Request.Path.Value, StringComparison.CurrentCultureIgnoreCase);
            if (isRequestingAppConfig)
            {
                string response = GenerateResponse();
                await context.Response.WriteAsync(response);
            }
            else
            {
                await _next.Invoke(context);
            }
        }

        private string GenerateResponse()
        {
            var cleanServerRootAddress = _serverRootAddress.TrimEnd('/');
            var cleanClientRootAddress = _clientRootAddress.TrimEnd('/');
            return $@"{{
    ""remoteServiceBaseUrl"": ""{cleanServerRootAddress}"",
    ""appBaseUrl"": ""{cleanClientRootAddress}""
    }}";
        }
    }

    public static class DynamicAppConfigMiddlewareBuilder
    {
        /// <summary>
        /// Dynamically generate the client-side config file appconfig.json so that it uses values from the
        /// server-side appsettings.json config file, so that the client can pull those values from
        /// Azure App Settings in production rather than getting them pushed in during packaging.
        ///
        /// This needs to be a middleware component because we need to intercept the request prior to
        /// UseStaticFiles() which is way before any MVC or other dynamic content usually gets a chance
        /// to act on the request
        /// </summary>
        public static IApplicationBuilder UseDynamicAppConfig(this IApplicationBuilder builder, IConfigurationRoot appConfiguration)
        {
            var serverRootAddress = appConfiguration["App:ServerRootAddress"];
            var clientRootAddress = appConfiguration["App:ClientRootAddress"];
            return builder.UseMiddleware<DynamicAppConfigMiddleware>(serverRootAddress, clientRootAddress);
        }
    }
}
