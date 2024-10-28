using System.ComponentModel.DataAnnotations;

namespace BudgetMaster.API.Models
{
    public class LoginModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = default!;

        [Required]
        public string Password { get; set; } = default!;
    }
}