using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cinema.Api.Models.Movies
{
    public class AddRequestModel : BaseRequestModel
    {
        public int Id { get; set; }
        public int SeanceId { get; set; }
        public int SalonId { get; set; }
        public string Name { get; set; }
        public int TypeId { get; set; }
        public string Director { get; set; }
        public string Banner { get; set; }
        
    }
}