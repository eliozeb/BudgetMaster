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
        private readonly IHttpContextAccessor? _httpContextAccessor;
        private readonly string? _tenantConnectionStringTemplate;

        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(
            DbContextOptions<ApplicationDbContext> options,
            IHttpContextAccessor? httpContextAccessor = null,
            IConfiguration? configuration = null)
            : base(options)
        {
            _httpContextAccessor = httpContextAccessor;
            _tenantConnectionStringTemplate = configuration?.GetConnectionString("TenantConnection") ?? string.Empty;
        }

        // In your ApplicationDbContext.cs
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var tenantId = _httpContextAccessor?.HttpContext?.Items["TenantId"]?.ToString();

                if (!string.IsNullOrEmpty(tenantId))
                {
                    var connectionString = GetConnectionString(tenantId);
                    optionsBuilder.UseSqlServer(connectionString);
                }
                else
                {
                    // Design-time scenario: use a default connection string
                    optionsBuilder.UseSqlServer("MasterConnection");
                }
            }
        }


        private string? GetConnectionString(string tenantId)
        {
            var tenantDbName = $"BudgetMaster_{tenantId}";
            return _tenantConnectionStringTemplate!.Replace("{DatabaseName}", tenantDbName);
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
