using Cinema.Web.Business.Models;
using Cinema.Web.Business.Models.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Web.Business.Interfaces
{
    public interface IAuthService
    {
        ApiResponseModel<PaginatedList<Auth>> GetAllPaginatedWithDetailBySearchFilter(string userToken, AuthSearchFilter searchFilter);

        ApiResponseModel<List<Auth>> GetAll(string userToken);
        ApiResponseModel<List<Auth>> GetAllByCurrentUser(string userToken);
        ApiResponseModel<Auth> GetById(string userToken, int id);
        ApiResponseModel<Auth> Add(string userToken, Auth auth);
        ApiResponseModel<Auth> Edit(string userToken, Auth auth);
        ApiResponseModel<Auth> Delete(string userToken, int authId);
    }
}
