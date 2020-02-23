using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cinema.Web.Models.User
{
    public class ListViewModel : ListBaseViewModel<Business.Models.User.UserWithDetail, ListFilterViewModel>
    {
        public List<SelectListItem> ProfileSelectList { get; set; }

    }
}