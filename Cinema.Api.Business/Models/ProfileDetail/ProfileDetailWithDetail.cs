using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Api.Business.Models.ProfileDetail
{
    public class ProfileDetailWithDetail
    {
        public long Id { get; set; }
        public int ProfileId { get; set; }
        public int AuthId { get; set; }

        //detail columns
        public string Profile_Name { get; set; }
        public string Auth_Name { get; set; }
    }
}
