using System.ComponentModel.DataAnnotations;

namespace SalaryCalc.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Имя пользователя")]
        public string UserName { get; set; }
        [Required]
        [Display(Name = "Пароль")]
        public string Password { get; set; }
        [Display(Name = "Запомнить данные для входа?")]
        public bool IsRemember { get; set; }
        [Required]
        public string ReturnUrl { get; set; }
    }
}
