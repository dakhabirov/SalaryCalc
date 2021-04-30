using SalaryCalc.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace SalaryCalc.ViewModels
{
    public class RegisterViewModel : User
    {
        [Required(ErrorMessage = "Поле должно быть установлено")]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Поле должно быть установлено")]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        [DataType(DataType.Password)]
        [Display(Name = "Подтверждение пароля")]
        public string PasswordConfirm { get; set; }

        [Required]
        public string ReturnUrl { get; set; }
    }
}
