using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Web.Business.Models.ProfileDetail
{
    public class DeleteByProfileIdAndAuthIdRequestModel : BaseRequestModel
    {
        public int ProfileId { get; set; }
        public int AuthId { get; set; }
    }
}
