using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cinema.Api.Models.User
{
    public class LogoutRequestModel
    {
        public string UserToken { get; set; }
    }
}