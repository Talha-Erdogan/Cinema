using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cinema.Web.Models.ProfileDetail
{
    public class BatchEditViewModel
    {
        public int ProfileId { get; set; }
        public List<SelectListItem> ProfileSelectList { get; set; }
        public List<AuthCheckViewModel> AuthList { get; set; }
        public List<AuthCheckViewModel> AuthWhichIsNotIncludeList { get; set; }

        public string SubmitType { get; set; }
    }
}