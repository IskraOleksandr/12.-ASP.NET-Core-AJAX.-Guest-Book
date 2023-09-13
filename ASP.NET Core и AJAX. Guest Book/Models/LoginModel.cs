using System.ComponentModel.DataAnnotations;

namespace ASP.NET_Core_и_AJAX._Guest_Book.Models
{
    // класс модели-представления (view-model)
    public class LoginModel
    {
        [Required]
        [Display(Name = "Логин:")]
        public string? Login { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль:")]
        public string? Password { get; set; }
    }
}
