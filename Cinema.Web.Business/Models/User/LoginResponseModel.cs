﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Web.Business.Models.User
{
    public class LoginResponseModel : User
    {
        public string UserToken { get; set; }
    }
}
