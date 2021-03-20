using Abp.Dependency;
using Abp.Runtime.Session;
using log4net;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace LeesStore.Web.Host.Startup
{
    public class SessionLoggingMiddleware : IMiddleware, ITransientDependency
    {
        private readonly IAbpSession _session;

        public SessionLoggingMiddleware(IAbpSession session)
        {
            _session = session;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            LogicalThreadContext.Properties["userid"] = _session.UserId;
            LogicalThreadContext.Properties["tenantid"] = _session.TenantId;
            await next(context);
        }
    }

    public static class SessionLoggingMiddlewareUtil
    {
        public static void UseSessionLogging(this IApplicationBuilder app)
        {
            app.UseMiddleware<SessionLoggingMiddleware>();
        }
    }
}