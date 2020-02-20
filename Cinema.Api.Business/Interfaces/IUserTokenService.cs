using Cinema.Api.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Api.Business.Interfaces
{
    public interface IUserTokenService
    {
        UserToken GetByTokenWhichIsValid(string token);
        UserToken GetByToken(string token);
        int Add(UserToken record);
        int Update(UserToken record);
    }
}
