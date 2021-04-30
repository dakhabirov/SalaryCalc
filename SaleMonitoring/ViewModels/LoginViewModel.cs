using SalaryCalc.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace SalaryCalc.ViewModels
{
    public class LoginViewModel : User
    {
        [Required]
        [Display(Name = "Пароль")]
        public string Password { get; set; }
        [Required]
        public string ReturnUrl { get; set; }
    }
}
