﻿using System.ComponentModel.DataAnnotations;

namespace AcademyWebEF.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Please enter email.")]
        [Display(Name = "Email")]
        [StringLength(50, ErrorMessage = "Max limit is 50 characters.")]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please enter password.")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        public bool RememberMe { get; set; }
    }
}