using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using BudgetMaster.Application.Interfaces;
using BudgetMaster.Infrastructure.Services;

namespace BudgetMaster.Infrastructure.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<ITenantService>(sp =>
            {
                // Use the IConfiguration instance from the service provider
                var config = sp.GetRequiredService<IConfiguration>();
                return new TenantService(config, sp);
            });

            // Register other Infrastructure services here
            // For example, if you have a UserRepository:
            // services.AddScoped<IUserRepository, UserRepository>();

            return services;
        }
    }
}
