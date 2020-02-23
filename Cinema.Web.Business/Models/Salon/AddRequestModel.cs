using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Web.Business.Models.Salon
{
    public class AddRequestModel : BaseRequestModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
