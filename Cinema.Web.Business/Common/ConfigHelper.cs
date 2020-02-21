using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Web.Business.Common
{
    public static class ConfigHelper
    {
        public static string ApiUrl { get { return System.Configuration.ConfigurationManager.AppSettings["ApiUrl"]; } }
    }
}
