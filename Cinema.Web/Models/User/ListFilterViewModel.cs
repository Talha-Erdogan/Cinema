using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cinema.Web.Models.User
{
    public class ListFilterViewModel
    { 
        // filters    
        public string Filter_Name { get; set; }
        public string Filter_Surname { get; set; }
        public int? Filter_ProfileId { get; set; }
    }
}