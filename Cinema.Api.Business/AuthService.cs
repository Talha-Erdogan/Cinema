using Cinema.Api.Business.Models;
using Cinema.Api.Business.Models.Auth;
using Cinema.Api.Data;
using Cinema.Api.Data.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Dynamic;
using Cinema.Api.Business.Interfaces;

namespace Cinema.Api.Business
{
    public class AuthService: IAuthService
    {
        public PaginatedList<Auth> GetAllPaginatedBySearchFilter(AuthSearchFilter searchFilter)
        {
            PaginatedList<Auth> resultList = new PaginatedList<Auth>(new List<Auth>(), 0, searchFilter.CurrentPage, searchFilter.PageSize, searchFilter.SortOn, searchFilter.SortDirection);

            using (AppDbContext dbContext = new AppDbContext())
            {
                var query = from a in dbContext.Auth
                            where a.IsDeleted == false
                            select new Auth()
                            {
                                Id = a.Id,
                                Name = a.Name,
                                Code = a.Code,
                                IsDeleted = a.IsDeleted,
                            };

                // filtering
                if (!string.IsNullOrEmpty(searchFilter.Filter_Name))
                {
                    query = query.Where(r => r.Name.Contains(searchFilter.Filter_Name));
                }

                if (!string.IsNullOrEmpty(searchFilter.Filter_Code))
                {
                    query = query.Where(r => r.Code.Contains(searchFilter.Filter_Code));
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


                resultList = new PaginatedList<Auth>(
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

        public List<Auth> GetAll()
        {
            List<Auth> resultList = new List<Auth>();

            using (AppDbContext dbContext = new AppDbContext())
            {
                var query = from a in dbContext.Auth
                            where a.IsDeleted == false
                            select new Auth()
                            {
                                Id = a.Id,
                                Name = a.Name,
                                Code = a.Code,
                                IsDeleted = a.IsDeleted,
                            };
                resultList = query.ToList();
            }

            return resultList;
        }

        public List<Auth> GetAllByProfileId(int profileId)
        {
            List<Auth> resultList = new List<Auth>();

            using (AppDbContext dbContext = new AppDbContext())
            {
                var query = from pd in dbContext.ProfileDetail
                            join p in dbContext.Profile on pd.ProfileId equals p.Id
                            join a in dbContext.Auth on pd.AuthId equals a.Id
                            where
                                pd.ProfileId == profileId && a.IsDeleted == false && p.IsDeleted == false
                            select a;

                // as no tracking
                query = query.AsNoTracking();
                resultList.AddRange(query.ToList());
            }
            return resultList;
        }

        public Auth GetById(int id)
        {
            Auth result = new Auth();

            using (AppDbContext dbContext = new AppDbContext())
            {
                result = dbContext.Auth.Where(r => r.Id == id && r.IsDeleted == false).AsNoTracking().SingleOrDefault();
            }

            return result;
        }

        public int Add(Auth record)
        {
            int result = 0;

            using (AppDbContext dbContext = new AppDbContext())
            {
                dbContext.Entry(record).State = EntityState.Added;
                result = dbContext.SaveChanges();
            }

            return result;
        }

        public int Update(Auth record)
        {
            int result = 0;

            using (AppDbContext dbContext = new AppDbContext())
            {
                dbContext.Entry(record).State = EntityState.Modified;
                result = dbContext.SaveChanges();
            }

            return result;
        }

        public int Delete(Auth record)
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
    }
}
