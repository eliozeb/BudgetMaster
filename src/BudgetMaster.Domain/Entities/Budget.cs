using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BudgetMaster.Domain.Entities
{
    public class Budget
    {
        [Key]
        public int BudgetId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = default!;

        [Required]
        public decimal Amount { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        public int? CategoryId { get; set; } // Optional, if budget is per category

        [ForeignKey("CategoryId")]
        public Category Category { get; set; } = default!;

        [Required]
        public string UserId { get; set; } = default!;
    }
}
