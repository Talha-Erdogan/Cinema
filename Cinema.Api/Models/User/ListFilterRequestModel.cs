using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cinema.Api.Models.User
{
    public class ListFilterRequestModel
    { 
        // filters    
        public string Filter_Name { get; set; }
        public string Filter_Surname { get; set; }
        public int? Filter_ProfileId { get; set; }
    }
}