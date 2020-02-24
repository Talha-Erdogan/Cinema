using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cinema.Web.Models.Movies
{
    public class ListFilterViewModel
    {
        // filters    
        public string Filter_Name { get; set; }
        public int? Filter_SeanceId { get; set; }
        public int? Filter_SalonId { get; set; }
    }
}