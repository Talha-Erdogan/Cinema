using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Web.Business.Models.ProfileDetail
{
    public class GetAllAuthByProfileIdRequestModel : BaseRequestModel
    {
        public int ProfileId { get; set; }
    }
}
