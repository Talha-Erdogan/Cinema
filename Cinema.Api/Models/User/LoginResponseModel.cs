using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cinema.Api.Models.User
{
    public class LoginResponseModel : Data.Entity.User
    {
        public string UserToken { get; set; }
    }
}