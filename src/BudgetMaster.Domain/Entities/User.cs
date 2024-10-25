using System.Collections.Generic;

namespace BudgetMaster.Domain.Entities
{
    public class User
    {
        public string Id { get; set; } = default!;// Match the type used in Identity
        public string FullName { get; set; } = default!;

        // Navigation properties
        public ICollection<Transaction> Transactions { get; set; } = default!;
        public ICollection<Budget> Budgets { get; set; } = default!;
        public ICollection<Notification> Notifications { get; set; } = default!;
        public ICollection<Category> Categories { get; set; } = default!;
    }
}
