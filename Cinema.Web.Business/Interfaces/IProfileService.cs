using Cinema.Web.Business.Models;
using Cinema.Web.Business.Models.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Web.Business.Interfaces
{
    public interface IProfileService
    {
        ApiResponseModel<Profile> GetById(string userToken, int id);
    }
}
