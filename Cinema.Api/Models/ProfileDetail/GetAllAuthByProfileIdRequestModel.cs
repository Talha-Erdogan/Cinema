using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cinema.Api.Models.ProfileDetail
{
    public class GetAllAuthByProfileIdRequestModel : BaseRequestModel
    {
        public int ProfileId { get; set; }
    }
}