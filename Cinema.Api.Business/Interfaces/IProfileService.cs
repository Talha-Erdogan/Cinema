using Cinema.Api.Business.Models;
using Cinema.Api.Business.Models.Profile;
using Cinema.Api.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Api.Business.Interfaces
{
    public interface IProfileService
    {
        PaginatedList<Profile> GetAllPaginatedBySearchFilter(ProfileSearchFilter searchFilter);
        List<Profile> GetAll();
        Profile GetById(int id);
        int Add(Profile record);
        int Update(Profile record);
        int Delete(Profile record);
    }
}
