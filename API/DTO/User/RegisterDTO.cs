﻿using System.ComponentModel.DataAnnotations;

namespace API.DTO.User
{
    public class RegisterDTO
    {
        [Required]
        public string? Username { get; set; }
        [Required]
        public string? Password { get; set; }
    }
}
