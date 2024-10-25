using BudgetMaster.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BudgetMaster.Infrastructure.Data.Configurations
{
    public class BudgetConfiguration : IEntityTypeConfiguration<Budget>
    {
        public void Configure(EntityTypeBuilder<Budget> builder)
        {
            // Configure properties
            builder.HasKey(b => b.BudgetId);

            builder.Property(b => b.Name)
                .IsRequired()
                .HasMaxLength(100);

            // Seed data
            builder.HasData(
                new Budget
                {
                    BudgetId = 1,
                    Name = "Monthly Expenses",
                    Amount = 2000m,
                    StartDate = new DateTime(2023, 1, 1),
                    EndDate = new DateTime(2023, 1, 31),
                    UserId = "seed-user-id",
                    // Other properties as needed
                }
            );
        }
    }
}
