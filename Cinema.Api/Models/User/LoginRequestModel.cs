﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cinema.Api.Models.User
{
    public class LoginRequestModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}