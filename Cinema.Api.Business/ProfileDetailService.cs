using Cinema.Api.Business.Interfaces;
using Cinema.Api.Business.Models;
using Cinema.Api.Business.Models.ProfileDetail;
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
    public class ProfileDetailService: IProfileDetailService
    {
        public List<ProfileDetail> GetAll()
        {
            List<ProfileDetail> resultList = new List<ProfileDetail>();

            using (AppDbContext dbContext = new AppDbContext())
            {
                var query = from pd in dbContext.ProfileDetail
                            join p in dbContext.Profile on pd.ProfileId equals p.Id
                            join a in dbContext.Auth on pd.AuthId equals a.Id
                            where
                                p.IsDeleted == false && a.IsDeleted == false
                            select pd;

                // as no tracking
                query = query.AsNoTracking();

                resultList.AddRange(query.ToList());
            }
            return resultList;
        }

        public PaginatedList<ProfileDetailWithDetail> GetAllPaginatedWithDetailBySearchFilter(ProfileDetailSearchFilter searchFilter)
        {
            PaginatedList<ProfileDetailWithDetail> resultList = new PaginatedList<ProfileDetailWithDetail>(new List<ProfileDetailWithDetail>(), 0, searchFilter.CurrentPage, searchFilter.PageSize, searchFilter.SortOn, searchFilter.SortDirection);

            using (AppDbContext dbContext = new AppDbContext())
            {

                var query = from pd in dbContext.ProfileDetail
                            join a in dbContext.Auth on pd.AuthId equals a.Id
                            join p in dbContext.Profile on pd.ProfileId equals p.Id
                            where a.IsDeleted == false && p.IsDeleted == false
                            select new ProfileDetailWithDetail()
                            {
                                Id = pd.Id,
                                AuthId = pd.AuthId,
                                ProfileId = pd.ProfileId,
                                Auth_Name = a.Name,
                                Profile_Name = p.Name,
                            };

                // filtering
                if (searchFilter.Filter_AuthId.HasValue)
                {
                    query = query.Where(r => r.AuthId == searchFilter.Filter_AuthId.Value);
                }

                if (searchFilter.Filter_ProfileId.HasValue)
                {
                    query = query.Where(r => r.ProfileId == searchFilter.Filter_ProfileId.Value);
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
                    // deefault sıralama vermek gerekiyor yoksa skip metodu hata veriyor ef 6'da -- 28.10.2019 15:40
                    // https://stackoverflow.com/questions/3437178/the-method-skip-is-only-supported-for-sorted-input-in-linq-to-entities
                    query = query.OrderBy(r => r.Id);
                }

                //paging
                query = query.Skip((searchFilter.CurrentPage - 1) * searchFilter.PageSize).Take(searchFilter.PageSize);


                resultList = new PaginatedList<ProfileDetailWithDetail>(
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

        public List<Auth> GetAllAuthByProfileId(int profileId)
        {
            List<Auth> resultList = new List<Auth>();

            using (AppDbContext dbContext = new AppDbContext())
            {
                var query = from pd in dbContext.ProfileDetail
                            join a in dbContext.Auth on pd.AuthId equals a.Id
                            join p in dbContext.Profile on pd.ProfileId equals p.Id
                            where a.IsDeleted == false && p.IsDeleted == false
                            where
                                pd.ProfileId == profileId
                            select a;

                // as no tracking
                query = query.AsNoTracking();

                resultList.AddRange(query.ToList());

            }
            return resultList;
        }

        public List<Auth> GetAllAuthByProfileIdWhichIsNotIncluded(int profileId)
        {
            List<Auth> resultList = new List<Auth>();

            using (AppDbContext dbContext = new AppDbContext())
            {
                var query = from pd in dbContext.ProfileDetail
                            join a in dbContext.Auth on pd.AuthId equals a.Id
                            join p in dbContext.Profile on pd.ProfileId equals p.Id
                            where
                                pd.ProfileId == profileId && a.IsDeleted == false && p.IsDeleted == false
                            select a;

                // as no tracking
                var queryIdList = query.AsNoTracking().Select(r => r.Id);

                var query2 = from a in dbContext.Auth
                             where !queryIdList.Contains(a.Id) && a.IsDeleted == false
                             select a;

                // as no tracking
                query2 = query2.AsNoTracking();

                resultList.AddRange(query2.ToList());
            }
            return resultList;
        }

        public ProfileDetail GetById(long id)
        {
            ProfileDetail result = null;

            using (AppDbContext dbContext = new AppDbContext())
            {
                var query = from pd in dbContext.ProfileDetail
                            join p in dbContext.Profile on pd.ProfileId equals p.Id
                            join a in dbContext.Auth on pd.AuthId equals a.Id
                            where
                                p.IsDeleted == false && a.IsDeleted == false && pd.Id == id
                            select pd;

                // as no tracking
                query = query.AsNoTracking();

                result = query.SingleOrDefault();
            }

            return result;
        }

        public ProfileDetail GetByProfileIdAndAuthId(int profileId, int authId)
        {
            ProfileDetail result = null;

            using (AppDbContext dbContext = new AppDbContext())
            {
                var query = from pd in dbContext.ProfileDetail
                            join p in dbContext.Profile on pd.ProfileId equals p.Id
                            join a in dbContext.Auth on pd.AuthId equals a.Id
                            where
                                p.IsDeleted == false && a.IsDeleted == false && pd.ProfileId == profileId && pd.AuthId == authId
                            select pd;

                // as no tracking
                query = query.AsNoTracking();

                result = query.SingleOrDefault();
            }

            return result;
        }

        public int Add(ProfileDetail record)
        {
            int result = 0;

            using (AppDbContext dbContext = new AppDbContext())
            {
                ProfileDetail existrecord = dbContext.ProfileDetail.Where(pd => pd.ProfileId == record.ProfileId && pd.AuthId == record.AuthId).FirstOrDefault();
                if (existrecord == null)
                {
                    dbContext.Entry(record).State = EntityState.Added;
                    result = dbContext.SaveChanges();
                }
            }

            return result;
        }

        public int Update(ProfileDetail record)
        {
            int result = 0;

            using (AppDbContext dbContext = new AppDbContext())
            {
                dbContext.Entry(record).State = EntityState.Modified;
                result = dbContext.SaveChanges();
            }

            return result;
        }

        public int DeleteByProfileIdAndAuthId(int profileId, int authId)
        {
            int result = 0;

            using (AppDbContext dbContext = new AppDbContext())
            {
                ProfileDetail record = dbContext.ProfileDetail.Where(pd => pd.ProfileId == profileId && pd.AuthId == authId).AsNoTracking().SingleOrDefault();

                dbContext.Entry(record).State = EntityState.Deleted;
                result = dbContext.SaveChanges();
            }

            return result;
        }


    }
}
