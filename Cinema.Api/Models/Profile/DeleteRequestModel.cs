using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cinema.Api.Models.Profile
{
    public class DeleteRequestModel : BaseRequestModel
    {
        public int Id { get; set; }
    }
}