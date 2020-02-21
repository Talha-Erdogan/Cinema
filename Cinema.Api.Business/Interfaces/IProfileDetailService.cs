using Cinema.Api.Business.Models;
using Cinema.Api.Business.Models.ProfileDetail;
using Cinema.Api.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Api.Business.Interfaces
{
    public interface IProfileDetailService
    {
        List<ProfileDetail> GetAll();
        PaginatedList<ProfileDetailWithDetail> GetAllPaginatedWithDetailBySearchFilter(ProfileDetailSearchFilter searchFilter);
        List<Auth> GetAllAuthByProfileId(int profileId);
        List<Auth> GetAllAuthByProfileIdWhichIsNotIncluded(int profileId);
        ProfileDetail GetById(long id);
        ProfileDetail GetByProfileIdAndAuthId(int profileId, int authId);
        int Add(ProfileDetail record);
        int Update(ProfileDetail record);
        int DeleteByProfileIdAndAuthId(int profileId, int authId);


    }
}
