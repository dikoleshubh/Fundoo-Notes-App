using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Common_Layer
{
    public class LoginModel
    {
        [Key]
        [RegularExpression(@"^[a-zA-Z0-9]{1,}([.]?[-]?[+]?[a-zA-Z0-9]{1,})?[@]{1}[a-zA-Z0-9]{1,}[.]{1}[a-z]{2,3}([.]?[a-z]{2})?$", ErrorMessage = "Invalid Email Id")]
        public string Email { get; set; }


        [Required]
        public string Password { get; set; }
    }
}
