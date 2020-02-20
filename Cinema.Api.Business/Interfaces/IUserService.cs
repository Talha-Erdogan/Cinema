using Cinema.Api.Business.Models;
using Cinema.Api.Business.Models.User;
using Cinema.Api.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Api.Business.Interfaces
{
    public interface IUserService
    {
        PaginatedList<UserWithDetail> GetAllPaginatedWithDetailBySearchFilter(UserSearchFilter searchFilter);
        List<UserWithDetail> GetAllWithDetail();
        User GetById(int id);
        int Add(User record);
        int Update(User record);
        int Delete(User record);
        User GetByUsernameAndPassword(string username, string password);
    }
}
