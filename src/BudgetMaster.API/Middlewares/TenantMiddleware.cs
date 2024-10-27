using System.Collections.Concurrent;
using BudgetMaster.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BudgetMaster.API.Middlewares
{
    public class TenantMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IDbContextFactory<ApplicationDbContext> _dbContextFactory;

        private static readonly ConcurrentDictionary<string, bool> _migrationsApplied = new ConcurrentDictionary<string, bool>();

        // Declare _migrationLock as a static readonly field
        private static readonly SemaphoreSlim _migrationLock = new SemaphoreSlim(1, 1);

        public TenantMiddleware(RequestDelegate next, IDbContextFactory<ApplicationDbContext> dbContextFactory)
        {
            _next = next;
            _dbContextFactory = dbContextFactory;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var tenantId = context.Request.Headers["Tenant-ID"].FirstOrDefault();

            if (string.IsNullOrEmpty(tenantId))
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                await context.Response.WriteAsync("Tenant ID is missing.");
                return;
            }

            context.Items["TenantId"] = tenantId;

            // Check and create tenant database if necessary
            await EnsureTenantDatabaseExistsAsync(tenantId);

            await _next(context);
        }

        /// <summary>
        /// Applying Migrations for Each Tenant's Database
        /// </summary>
        /// <param name="tenantId"></param>
        /// <returns></returns>
        private async Task EnsureTenantDatabaseExistsAsync(string tenantId)
        {
            if (_migrationsApplied.ContainsKey(tenantId))
            {
                return; // Migrations already applied for this tenant
            }

            using var context = _dbContextFactory.CreateDbContext();
            context.SetTenantId(tenantId);

            try
            {
                await _migrationLock.WaitAsync();

                // Apply migrations
                await context.Database.MigrateAsync();

                // Mark migrations as applied
                _migrationsApplied[tenantId] = true;
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine($"Error migrating tenant database for '{tenantId}': {ex.Message}");
                throw;
            }
            finally
            {
                _migrationLock.Release();
            }
        }

    }
}
