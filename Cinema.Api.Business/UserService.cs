using Cinema.Api.Business.Interfaces;
using Cinema.Api.Business.Models;
using Cinema.Api.Business.Models.User;
using Cinema.Api.Data;
using Cinema.Api.Data.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Dynamic;

namespace Cinema.Api.Business
{
    public class UserService : IUserService
    {
        public PaginatedList<UserWithDetail> GetAllPaginatedWithDetailBySearchFilter(UserSearchFilter searchFilter)
        {
            PaginatedList<UserWithDetail> resultList = new PaginatedList<UserWithDetail>(new List<UserWithDetail>(), 0, searchFilter.CurrentPage, searchFilter.PageSize, searchFilter.SortOn, searchFilter.SortDirection);

            using (AppDbContext dbContext = new AppDbContext())
            {

                var query = from u in dbContext.User
                            from p in dbContext.Profile.Where(p => p.Id == u.ProfileId && p.IsDeleted == false).DefaultIfEmpty()
                            where u.IsDeleted == false
                            select new UserWithDetail()
                            {
                                Id = u.Id,
                                UserName = u.UserName,
                                Password = u.Password,
                                Name = u.Name,
                                Surname = u.Surname,
                                Mail = u.Mail,
                                ProfileId = u.ProfileId,
                               
                                Profile_Name = p == null ? String.Empty : p.Name,
                            };

                // filtering
                if (searchFilter.Filter_ProfileId.HasValue)
                {
                    query = query.Where(r => r.ProfileId == searchFilter.Filter_ProfileId.Value);
                }

                if (!string.IsNullOrEmpty(searchFilter.Filter_Name))
                {
                    query = query.Where(r => r.Name.Contains(searchFilter.Filter_Name));
                }

                if (!string.IsNullOrEmpty(searchFilter.Filter_Surname))
                {
                    query = query.Where(r => r.Surname.Contains(searchFilter.Filter_Surname));
                }

                // asnotracking
                query = query.AsNoTracking();

                //total count
                var totalCount = query.Count();

                //sorting
                if (!string.IsNullOrEmpty(searchFilter.SortOn))
                {
                    // System.Linq.Dynamic nuget paketi ve namespace eklenmelidir, dynamic order by yapmak icindir
                    query = query.OrderBy(searchFilter.SortOn + " " + searchFilter.SortDirection.ToUpper());
                }
                else
                {
                    // deefault sıralama vermek gerekiyor yoksa skip metodu hata veriyor ef 6'da 
                    // https://stackoverflow.com/questions/3437178/the-method-skip-is-only-supported-for-sorted-input-in-linq-to-entities
                    query = query.OrderBy(r => r.Id);
                }

                //paging
                query = query.Skip((searchFilter.CurrentPage - 1) * searchFilter.PageSize).Take(searchFilter.PageSize);


                resultList = new PaginatedList<UserWithDetail>(
                    query.ToList(),
                    totalCount,
                    searchFilter.CurrentPage,
                    searchFilter.PageSize,
                    searchFilter.SortOn,
                    searchFilter.SortDirection
                    );
            }

            return resultList;
        }

        public List<UserWithDetail> GetAllWithDetail()
        {
            List<UserWithDetail> resultList = new List<UserWithDetail>();

            using (AppDbContext dbContext = new AppDbContext())
            {
                var query = from u in dbContext.User
                            from p in dbContext.Profile.Where(p => p.Id == u.ProfileId && p.IsDeleted == false).DefaultIfEmpty()
                            where u.IsDeleted == false
                            select new UserWithDetail()
                            {
                                Id = u.Id,
                                UserName = u.UserName,
                                Password =u.Password,
                                Name = u.Name,
                                Surname = u.Surname,
                                Mail = u.Mail,
                                ProfileId = u.ProfileId,

                                Profile_Name = p == null ? String.Empty : p.Name,
                            };
                resultList = query.ToList();

            }

            return resultList;
        }

        public User GetById(int id)
        {
            User result = new User();
            using (AppDbContext dbContext = new AppDbContext())
            {
                result = dbContext.User.Where(r => r.Id == id && r.IsDeleted == false).AsNoTracking().SingleOrDefault();
            }
            return result;
        }

        public int Add(User record)
        {
            int result = 0;

            using (AppDbContext dbContext = new AppDbContext())
            {
                dbContext.Entry(record).State = EntityState.Added;
                result = dbContext.SaveChanges();
            }

            return result;
        }

        public int Update(User record)
        {
            int result = 0;

            using (AppDbContext dbContext = new AppDbContext())
            {
                dbContext.Entry(record).State = EntityState.Modified;
                result = dbContext.SaveChanges();
            }

            return result;
        }

        public int Delete(User record)
        {
            int result = 0;

            using (AppDbContext dbContext = new AppDbContext())
            {
                record.IsDeleted = true;
                dbContext.Entry(record).State = EntityState.Modified;
                result = dbContext.SaveChanges();
            }

            return result;
        }

        public User GetByUsernameAndPassword(string username,string password)
        {
            User result = new User();
            using (AppDbContext dbContext = new AppDbContext())
            {
                result = dbContext.User.Where(r => r.UserName == username && r.Password == password && r.IsDeleted == false).AsNoTracking().SingleOrDefault();
            }
            return result;
        }

    }
}
