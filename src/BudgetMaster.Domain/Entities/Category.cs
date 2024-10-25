using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BudgetMaster.Domain.Entities
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = default!;

        [MaxLength(500)]
        public string Description { get; set; } = default!;

        [Required]
        [MaxLength(10)]
        public string Type { get; set; } = default!; // "Expense" or "Income"

        [Required]
        public string UserId { get; set; } = default!;

        // Navigation properties
        public ICollection<Transaction> Transactions { get; set; } = default!;
    }
}
