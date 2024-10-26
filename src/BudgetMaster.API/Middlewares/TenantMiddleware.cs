using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace BudgetMaster.API.Middlewares
{
    public class TenantMiddleware
    {
        private readonly RequestDelegate _next;
        public TenantMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Extract tenant ID from header, query, or route
            var tenantId = context.Request.Headers["Tenant-ID"].FirstOrDefault();

            if (string.IsNullOrEmpty(tenantId))
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                await context.Response.WriteAsync("Tenant ID is missing.");
                return;
            }

            // Store tenant ID in HttpContext for later use
            context.Items["TenantId"] = tenantId;

            await _next(context);
        }
    }
}
