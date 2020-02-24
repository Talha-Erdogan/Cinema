using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Web.Business.Models.Movies
{
    public class ListFilterRequestModel
    {
        // filters    
        public string Filter_Name { get; set; }
        public int? Filter_SeanceId { get; set; }
        public int? Filter_SalonId { get; set; }
    }
}
