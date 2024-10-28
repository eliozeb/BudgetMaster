// BudgetMaster.API/Models/RegisterModel.cs
using System.ComponentModel.DataAnnotations;

namespace BudgetMaster.API.Models
{
    public class RegisterModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = default!;

        [Required]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters.")]
        public string Password { get; set; } = default!;

        [Required]
        public string FullName { get; set; } = default!;// New property
    }
}
