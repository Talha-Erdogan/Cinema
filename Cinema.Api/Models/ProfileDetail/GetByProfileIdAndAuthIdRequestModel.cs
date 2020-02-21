using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cinema.Api.Models.ProfileDetail
{
    public class GetByProfileIdAndAuthIdRequestModel : BaseRequestModel
    {
        public int ProfileId { get; set; }
        public int AuthId { get; set; }
    }
}