﻿using System.ComponentModel.DataAnnotations;

namespace CafeManagement.Dtos.Request
{
    public class CreateAccountRequest
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(6)]
        public string Password { get; set; }
    }
}
