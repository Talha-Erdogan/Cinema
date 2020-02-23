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

        ApiResponseModel<PaginatedList<UserWithDetail>> GetAllPaginatedWithDetailBySearchFilter(string userToken, UserSearchFilter searchFilter);
        ApiResponseModel<List<User>> GetAll(string userToken);
        ApiResponseModel<User> GetById(string userToken, int id);
        ApiResponseModel<User> Add(string userToken, User user);
        ApiResponseModel<User> Edit(string userToken, User user);
        ApiResponseModel<User> Delete(string userToken, int userId);
    }
}
