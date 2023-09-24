using System.ComponentModel.DataAnnotations;

namespace ASP.NET_Core_и_AJAX._Guest_Book.Models
{ 
    public class RegisterModel
    {
        [Required(ErrorMessage = "Поле Логин является обязательным.")]
        [Display(Name = "Логин:")]
        [StringLength(25, MinimumLength = 3, ErrorMessage = "Длина логина должна быть от 3 до 25 символов")]
        public string? Login { get; set; }

        [Required(ErrorMessage = "Поле Пароль является обязательным.")]
        [Display(Name = "Пароль:")]
        [DataType(DataType.Password)]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Длина пароля должна быть от 3 до 20 символов")]
        public string? Password { get; set; }

        [Required(ErrorMessage = "Поле подтверждения пароля является обязательным.")]
        [Display(Name = "Повторить пароль:")]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        [DataType(DataType.Password)]
        public string? PasswordConfirm { get; set; }
    }
}