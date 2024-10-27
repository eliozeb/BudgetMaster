using BudgetMaster.Infrastructure.Data;
using BudgetMaster.Infrastructure.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace BudgetMaster.Infrastructure.Data
{
    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            // Determine the base path for configuration files
            var basePath = Directory.GetCurrentDirectory();

            // Adjust the base path if necessary
            // If running from the Infrastructure project directory, move up to the solution directory
            if (File.Exists(Path.Combine(basePath, "BudgetMaster.Infrastructure.csproj")))
            {
                basePath = Path.Combine(basePath, "..", "BudgetMaster.API");
            }

            // Build configuration
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development";

            var configuration = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{environment}.json", optional: true)
                .AddEnvironmentVariables()
                .Build();

            // Get connection string for design-time
            var connectionString = configuration.GetConnectionString("MasterConnection");

            // Set up DbContextOptions
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseSqlServer(connectionString);

            // Create instance of ApplicationDbContext
            return new ApplicationDbContext(optionsBuilder.Options, configuration);
        }
    }
}
