using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ModelLayer.Model
{
    public class ForgotPasswordRequestModel
    {
        [Required]
        public string email {  get; set; }
    }
}
