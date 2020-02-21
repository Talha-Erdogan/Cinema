using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Web.Business.Models
{
    public class BaseResponseModel
    {
        public string ResultStatusCode { get; set; }
        public string ResultStatusMessage { get; set; }
    }
}
