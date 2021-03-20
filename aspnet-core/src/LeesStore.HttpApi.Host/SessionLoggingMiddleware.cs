using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.MultiTenancy;
using Volo.Abp.Users;

namespace LeesStore
{
    public class SessionLoggingMiddleware : IMiddleware, ITransientDependency
    {
        private readonly ICurrentUser _currentUser;
        private readonly ICurrentTenant _currentTenant;

        public SessionLoggingMiddleware(ICurrentUser currentUser, ICurrentTenant currentTenant)
        {
            _currentUser = currentUser;
            _currentTenant = currentTenant;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            using (Serilog.Context.LogContext.PushProperty("UserId", _currentUser.Id))
            using (Serilog.Context.LogContext.PushProperty("TenantId", _currentTenant.Id))
            {
                await next(context);
            }
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