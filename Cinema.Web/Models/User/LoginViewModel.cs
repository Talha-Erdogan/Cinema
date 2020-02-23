using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Cinema.Web.Models.User
{
    public class LoginViewModel
    {
        [Required(ErrorMessage ="Username")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password")]
        public string  Password { get; set; }
    }
}