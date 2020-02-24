using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Web.Business.Models.Movies
{
    public class MoviesWithDetail
    {
        public int Id { get; set; }

        public int SeanceId { get; set; }

        public int SalonId { get; set; }

        public string Name { get; set; }
        public int TypeId { get; set; }

        public string Director { get; set; }
        public string Banner { get; set; }
        public bool IsDeleted { get; set; }

        //detail columns
        public string Seance_Name { get; set; }
        public string Salon_Name { get; set; }
        public string Type_Name { get; set; }
    }
}
