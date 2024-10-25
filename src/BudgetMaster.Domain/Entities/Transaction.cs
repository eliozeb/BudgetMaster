using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BudgetMaster.Domain.Entities
{
    public class Transaction
    {
        [Key]
        public int TransactionId { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        [MaxLength(10)]
        public string Type { get; set; } = default!; // "Expense" or "Income"

        [MaxLength(1000)]
        public string Notes { get; set; } = default!;

        [MaxLength(200)]
        public string ReceiptPath { get; set; } = default!;

        [Required]
        public int CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public Category Category { get; set; } = default!;

        [Required]
        public string UserId { get; set; } = default!;
    }
}
