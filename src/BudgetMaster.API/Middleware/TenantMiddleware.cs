using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace BudgetMaster.API.Middleware
{
    public class TenantMiddleware
    {
        private readonly RequestDelegate _next = default!;

        public TenantMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.Request.Headers.TryGetValue("X-Tenant-ID", out var tenantId))
            {
                context.Items["TenantId"] = tenantId.ToString();
            }
            else
            {
                // Handle missing tenant ID
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                await context.Response.WriteAsync("Tenant ID is missing.");
                return;
            }

            await _next(context);
        }
    }
}
