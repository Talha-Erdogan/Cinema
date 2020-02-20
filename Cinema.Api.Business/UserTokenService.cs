using Cinema.Api.Business.Interfaces;
using Cinema.Api.Data;
using Cinema.Api.Data.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Api.Business
{
    public class UserTokenService: IUserTokenService
    {
        public UserToken GetByTokenWhichIsValid(string token)
        {
            UserToken userToken = null;

            using (AppDbContext dbContext = new AppDbContext())
            {
                userToken = dbContext.UserToken.Where(r => r.Token == token && r.IsValid == true && r.ValidEndDate >= DateTime.Now).AsNoTracking().SingleOrDefault();
            }

            return userToken;
        }

        public UserToken GetByToken(string token)
        {
            UserToken userToken = null;
            using (AppDbContext dbContext = new AppDbContext())
            {
                userToken = dbContext.UserToken.Where(r => r.Token == token).AsNoTracking().SingleOrDefault();
            }
            return userToken;
        }

        public int Add(UserToken record)
        {
            int result = 0;

            using (AppDbContext dbContext = new AppDbContext())
            {
                dbContext.Entry(record).State = EntityState.Added;
                result = dbContext.SaveChanges();
            }

            return result;
        }

        public int Update(UserToken record)
        {
            int result = 0;

            using (AppDbContext dbContext = new AppDbContext())
            {
                dbContext.Entry(record).State = EntityState.Modified;
                result = dbContext.SaveChanges();
            }

            return result;
        }

    }
}
