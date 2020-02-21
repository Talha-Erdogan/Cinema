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
        ApiResponseModel<PaginatedList<Profile>> GetAllPaginatedWithDetailBySearchFilter(string userToken, ProfileSearchFilter searchFilter);
        ApiResponseModel<List<Profile>> GetAll(string userToken);
        ApiResponseModel<Profile> GetById(string userToken, int id);
        ApiResponseModel<Profile> Add(string userToken, Profile profile);
        ApiResponseModel<Profile> Edit(string userToken, Profile profile);
        ApiResponseModel<Profile> Delete(string userToken, int profileId);
    }
}
