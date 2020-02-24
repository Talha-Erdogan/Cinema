using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cinema.Web.Models.Movies
{
    public class AddViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="Seance is required.")]
        public int SeanceId { get; set; }

        [Required(ErrorMessage = "Salon is required.")]
        public int SalonId { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Type is required.")]
        public int TypeId { get; set; }

        [Required(ErrorMessage = "Director is required.")]
        public string Director { get; set; }

        public string Banner { get; set; }


        //select list
        public List<SelectListItem> SeanceSelectList { get; set; }
        public List<SelectListItem> SalonSelectList { get; set; }
        public List<SelectListItem> MoviesTypeSelectList { get; set; }
    }
}