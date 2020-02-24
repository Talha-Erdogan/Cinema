using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cinema.Web.Models.Movies
{
    public class ListViewModel : ListBaseViewModel<Business.Models.Movies.MoviesWithDetail, ListFilterViewModel>
    {
        //select list
        public List<SelectListItem> SeanceSelectList { get; set; }
        public List<SelectListItem>SalonSelectList { get; set; }
    }
}