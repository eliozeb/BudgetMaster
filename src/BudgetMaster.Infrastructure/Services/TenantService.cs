using BudgetMaster.Application.Interfaces;
using BudgetMaster.Infrastructure.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace BudgetMaster.Infrastructure.Services
{
    public class TenantService : ITenantService
    {
        private readonly string _masterConnectionString = default!;
        private readonly string _tenantConnectionStringTemplate = default!;
        private readonly IServiceProvider _serviceProvider = default!;

        public TenantService(IConfiguration configuration, IServiceProvider serviceProvider)
        {
            _masterConnectionString = configuration.GetConnectionString("MasterConnection") ?? string.Empty;
            _tenantConnectionStringTemplate = configuration.GetConnectionString("TenantConnection") ?? string.Empty;
            _serviceProvider = serviceProvider;
        }

        public async Task CreateTenantAsync(string tenantId)
        {
            var tenantDbName = $"BudgetMaster_{tenantId}";

            await using var connection = new SqlConnection(_masterConnectionString);
            var createDbCommand = $"IF DB_ID('{tenantDbName}') IS NULL CREATE DATABASE [{tenantDbName}];";
            var command = new SqlCommand(createDbCommand, connection);

            await connection.OpenAsync();
            await command.ExecuteNonQueryAsync();

            // Apply migrations to the new tenant database
            await ApplyMigrationsAsync(tenantId);
        }

        private async Task ApplyMigrationsAsync(string tenantId)
        {
            var connectionString = GetConnectionString(tenantId);

            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseSqlServer(connectionString);

            using var scope = _serviceProvider.CreateScope();
            var context = ActivatorUtilities.CreateInstance<ApplicationDbContext>(scope.ServiceProvider, optionsBuilder.Options);

            await context.Database.MigrateAsync();
        }

        private string GetConnectionString(string tenantId)
        {
            var tenantDbName = $"BudgetMaster_{tenantId}";
            return _tenantConnectionStringTemplate.Replace("{DatabaseName}", tenantDbName);
        }
    }
}
