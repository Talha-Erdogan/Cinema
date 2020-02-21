using Cinema.Web.Business.Models.Auth;
using Cinema.Web.Business.Models.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Web.Business.Common.Session
{
    public class SessionUser
    {
        public string UserToken { get; set; }
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Mail { get; set; }
        public int ProfileId { get; set; }

        //detail information for user session
        public List<Auth> UserAuthList { get; set; }
        public Profile Profile { get; set; }
    }
}
