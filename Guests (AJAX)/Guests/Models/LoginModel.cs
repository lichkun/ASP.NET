﻿using System.ComponentModel.DataAnnotations;

namespace StoringPassword.Models
{
    public class LoginModel
    {
        [Required]
        public string? Login { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string? Password { get; set; }
    }
}