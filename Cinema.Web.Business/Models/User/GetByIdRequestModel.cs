using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Web.Business.Models.User
{
    public class GetByIdRequestModel : BaseRequestModel
    {
        public int Id { get; set; }
    }
}
