using Cinema.Web.Business.Models;
using Cinema.Web.Business.Models.ProfileDetail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Web.Business.Interfaces
{
    public interface IProfileDetailService
    {
        ApiResponseModel<List<Business.Models.Auth.Auth>> GetAllAuthByProfileId(string userToken, int profileId);
        ApiResponseModel<List<Business.Models.Auth.Auth>> GetAllAuthByProfileIdWhichIsNotIncluded(string userToken, int profileId);
        ApiResponseModel<ProfileDetail> Add(string userToken, ProfileDetail profileDetail);
        ApiResponseModel<int> DeleteByProfileIdAndAuthId(string userToken, int profileId, int authId);
    }
}
