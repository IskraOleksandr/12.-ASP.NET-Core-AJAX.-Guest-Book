﻿using System.ComponentModel.DataAnnotations;

namespace ASP.NET_Core_и_AJAX._Guest_Book.Models
{
    // класс модели-представления (view-model)
    public class RegisterModel
    {
        [Required]
        public string? Login { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        [Required]
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        [DataType(DataType.Password)]
        public string? PasswordConfirm { get; set; }
    }
}