using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cinema.Web.Models.User
{
    public class AddViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "User Name is required.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Surname is required.")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Mail is required.")]
        public string Mail { get; set; }

        [Required(ErrorMessage = "Profile is required.")]
        public int ProfileId { get; set; }


        //select list
        public List<SelectListItem> ProfileSelectList { get; set; }
    }
}