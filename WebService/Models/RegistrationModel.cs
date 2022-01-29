﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebService.Models
{
    public class RegistrationModel
    {
        [Required(ErrorMessage ="invalid Email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "invalid password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required(ErrorMessage = "password doesnt match")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}
