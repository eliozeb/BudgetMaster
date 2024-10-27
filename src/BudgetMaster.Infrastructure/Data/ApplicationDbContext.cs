using BudgetMaster.Domain.Entities;
using BudgetMaster.Infrastructure.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace BudgetMaster.Infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        private readonly IConfiguration? _configuration;
        private string? _tenantId;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IConfiguration configuration)
            : base(options)
        {
            _configuration = configuration;
        }

        public void SetTenantId(string tenantId)
        {
            _tenantId = tenantId;
        }

        // In your ApplicationDbContext.cs
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                if (string.IsNullOrEmpty(_tenantId))
                {
                    // Design-time context creation
                    var tenantConnectionStringTemplate = _configuration!.GetConnectionString("MasterConnection");
                    if (string.IsNullOrEmpty(tenantConnectionStringTemplate))
                    {
                        throw new InvalidOperationException("DefaultConnection string is not defined in the configuration.");
                    }

                    optionsBuilder.UseSqlServer(tenantConnectionStringTemplate);
                }
                else
                {
                    // Runtime context creation with tenant ID
                    var tenantConnectionStringTemplate = _configuration!.GetConnectionString("TenantConnection")
                        ?? throw new InvalidOperationException("TenantConnection string is not defined in the configuration.");

                    var connectionString = GetConnectionString(_tenantId, tenantConnectionStringTemplate);

                    optionsBuilder.UseSqlServer(connectionString, sqlOptions =>
                    {
                        sqlOptions.EnableRetryOnFailure();
                    });
                }
            }
        }



        private string GetConnectionString(string tenantId, string connectionStringTemplate)
        {
            var tenantDbName = $"BudgetMaster_{tenantId}";
            return connectionStringTemplate.Replace("{DatabaseName}", tenantDbName);
        }

        // DbSets for your entities
        public DbSet<Category> Categories { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Budget> Budgets { get; set; }
        public DbSet<Notification> Notifications { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Configure relationships between domain entities and ApplicationUser
            builder.Entity<Budget>()
                .HasOne<ApplicationUser>()
                .WithMany()
                .HasForeignKey(b => b.UserId)
                .IsRequired();

            builder.Entity<Budget>(entity =>
            {
                entity.Property(b => b.Amount)
                    .HasPrecision(18, 2);
            });

            builder.Entity<Transaction>(entity =>
            {
                entity.Property(t => t.Amount)
                    .HasPrecision(18, 2);
            });

            // Apply configurations from separate classes
            builder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        }
    }
}
