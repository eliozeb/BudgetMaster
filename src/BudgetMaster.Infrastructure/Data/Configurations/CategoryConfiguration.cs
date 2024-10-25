using BudgetMaster.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BudgetMaster.Infrastructure.Data.Configurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(c => c.CategoryId);

            builder.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(c => c.Type)
                .IsRequired()
                .HasMaxLength(10);

            // Additional configurations...

            // Seed data
            builder.HasData(
                new Category
                {
                    CategoryId = 1,
                    Name = "Salary",
                    Description = "Monthly salary from employer",
                    Type = "Income",
                    UserId = "seed-user-id"
                },
                new Category
                {
                    CategoryId = 2,
                    Name = "Groceries",
                    Description = "Expenses for groceries and household items",
                    Type = "Expense",
                    UserId = "seed-user-id"
                }
                // Add more categories as needed
            );
        }
    }
}
