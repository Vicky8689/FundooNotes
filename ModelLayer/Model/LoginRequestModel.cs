﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ModelLayer.Model
{
    public class LoginRequestModel
    { 
            [Required]
            public string Email { get; set; }
            [Required]
            public string Password { get; set; }
    }
}
