using BudgetMaster.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace BudgetMaster.Infrastructure.Identity
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public string FullName { get; set; } = default!;

        // Navigation properties
        public ICollection<Transaction> Transactions { get; set; } = default!;
        public ICollection<Budget> Budgets { get; set; } = default!;
        public ICollection<Notification> Notifications { get; set; } = default!;
        public ICollection<Category> Categories { get; set; } = default!;
    }
}
