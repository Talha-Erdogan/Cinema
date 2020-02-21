using Cinema.Web.Business.Models;
using Cinema.Web.Business.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Web.Business.Interfaces
{
    public interface IUserService
    {
        ApiResponseModel<LoginResponseModel> Login(string username, string password);
        ApiResponseModel<int> Logout();
    }
}
