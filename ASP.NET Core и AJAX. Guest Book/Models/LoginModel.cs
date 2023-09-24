using System.ComponentModel.DataAnnotations;

namespace ASP.NET_Core_и_AJAX._Guest_Book.Models
{ 
    public class LoginModel
    {
        [Required(ErrorMessage = "Поле Логин является обязательным.")]
        [Display(Name = "Логин:")]
        public string? Login { get; set; }

        [Required(ErrorMessage = "Поле Пароль является обязательным.")]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль:")]
        public string? Password { get; set; }
    }
}
