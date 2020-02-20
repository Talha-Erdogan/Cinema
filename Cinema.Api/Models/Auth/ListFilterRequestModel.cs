using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cinema.Api.Models.Auth
{
    public class ListFilterRequestModel
    {
        // filters    
        public string Filter_Name { get; set; }
        public string Filter_Code { get; set; }
    }
}