using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Web.Business.Models.ProfileDetail
{
    public class AddRequestModel : BaseRequestModel
    {
        public long Id { get; set; }
        public int ProfileId { get; set; }
        public int AuthId { get; set; }
    }
}
