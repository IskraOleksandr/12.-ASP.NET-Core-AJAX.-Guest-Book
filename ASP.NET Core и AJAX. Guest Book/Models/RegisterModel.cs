using System.ComponentModel.DataAnnotations;

namespace ASP.NET_Core_и_AJAX._Guest_Book.Models
{ 
    public class RegisterModel
    {
        [Required]
        [Display(Name = "Логин:")]
        [StringLength(25, MinimumLength = 3, ErrorMessage = "Длина логина должна быть от 3 до 25 символов")]
        public string? Login { get; set; }

        [Required]
        [Display(Name = "Пароль:")]
        [DataType(DataType.Password)]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Длина пароля должна быть от 3 до 20 символов")]
        public string? Password { get; set; }

        [Required]
        [Display(Name = "Повторить пароль:")]
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        [DataType(DataType.Password)]
        public string? PasswordConfirm { get; set; }
    }
}