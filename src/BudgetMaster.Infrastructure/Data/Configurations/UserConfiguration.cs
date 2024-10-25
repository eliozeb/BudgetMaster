using BudgetMaster.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace BudgetMaster.Infrastructure.Data.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            var hasher = new PasswordHasher<ApplicationUser>();
            var user = new ApplicationUser
            {
                Id = "seed-user-id",
                UserName = "john.doe@example.com",
                NormalizedUserName = "JOHN.DOE@EXAMPLE.COM",
                Email = "john.doe@example.com",
                NormalizedEmail = "JOHN.DOE@EXAMPLE.COM",
                EmailConfirmed = true,
                FullName = "John Doe",
                SecurityStamp = Guid.NewGuid().ToString("D")
            };
            user.PasswordHash = hasher.HashPassword(user, "Password123!");

            builder.HasData(user);
        }
    }
}
