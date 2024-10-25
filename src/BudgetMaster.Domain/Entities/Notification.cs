using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BudgetMaster.Domain.Entities
{
    public class Notification
    {
        [Key]
        public int NotificationId { get; set; }

        [Required]
        [MaxLength(500)]
        public string Message { get; set; } = default!;

        [Required]
        public DateTime DateCreated { get; set; }

        public bool IsRead { get; set; } = false;

        [Required]
        public string UserId { get; set; } = default!;
    }
}
