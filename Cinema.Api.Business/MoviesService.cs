using Cinema.Api.Business.Models;
using Cinema.Api.Business.Models.Movies;
using Cinema.Api.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Dynamic;
using Cinema.Api.Business.Interfaces;
using Cinema.Api.Data.Entity;

namespace Cinema.Api.Business
{
    public class MoviesService : IMoviesService
    {
        public PaginatedList<MoviesWithDetail> GetAllPaginatedWithDetailBySearchFilter(MoviesSearchFilter searchFilter)
        {
            PaginatedList<MoviesWithDetail> resultList = new PaginatedList<MoviesWithDetail>(new List<MoviesWithDetail>(), 0, searchFilter.CurrentPage, searchFilter.PageSize, searchFilter.SortOn, searchFilter.SortDirection);

            using (AppDbContext dbContext = new AppDbContext())
            {

                var query = from m in dbContext.Movies
                            from mt in dbContext.MoviesType.Where(mt => mt.Id == m.TypeId && mt.IsDeleted == false).DefaultIfEmpty()
                            from s in dbContext.Seance.Where(s => s.Id == m.SeanceId && s.IsDeleted == false).DefaultIfEmpty()
                            from sa in dbContext.Salon.Where(sa => sa.Id == m.SalonId && sa.IsDeleted == false).DefaultIfEmpty()
                            where m.IsDeleted == false
                            select new MoviesWithDetail()
                            {
                                Id = m.Id,
                                SeanceId = m.SeanceId,
                                SalonId = m.SalonId,
                                Name = m.Name,
                                TypeId = m.TypeId,
                                Director = m.Director,
                                Banner = m.Banner,
                                IsDeleted = m.IsDeleted,

                                Type_Name = mt == null ? String.Empty : mt.Name,
                                Seance_Name = s == null ? String.Empty : s.Name,
                                Salon_Name = sa == null ? String.Empty : sa.Name,
                            };

                // filtering
                if (searchFilter.Filter_SeanceId.HasValue)
                {
                    query = query.Where(r => r.SeanceId == searchFilter.Filter_SeanceId.Value);
                }

                if (searchFilter.Filter_SalonId.HasValue)
                {
                    query = query.Where(r => r.SalonId == searchFilter.Filter_SalonId.Value);
                }

                if (!string.IsNullOrEmpty(searchFilter.Filter_Name))
                {
                    query = query.Where(r => r.Name.Contains(searchFilter.Filter_Name));
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


                resultList = new PaginatedList<MoviesWithDetail>(
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

        public List<MoviesWithDetail> GetAllWithDetail()
        {
            List<MoviesWithDetail> resultList = new List<MoviesWithDetail>();

            using (AppDbContext dbContext = new AppDbContext())
            {
                var query = from m in dbContext.Movies
                            from mt in dbContext.MoviesType.Where(mt => mt.Id == m.TypeId && mt.IsDeleted == false).DefaultIfEmpty()
                            from s in dbContext.Seance.Where(s => s.Id == m.SeanceId && s.IsDeleted == false).DefaultIfEmpty()
                            from sa in dbContext.Salon.Where(sa => sa.Id == m.SalonId && sa.IsDeleted == false).DefaultIfEmpty()
                            where m.IsDeleted == false
                            select new MoviesWithDetail()
                            {
                                Id = m.Id,
                                SeanceId = m.SeanceId,
                                SalonId = m.SalonId,
                                Name = m.Name,
                                TypeId = m.TypeId,
                                Director = m.Director,
                                Banner = m.Banner,
                                IsDeleted = m.IsDeleted,

                                Type_Name = mt == null ? String.Empty : mt.Name,
                                Seance_Name = s == null ? String.Empty : s.Name,
                                Salon_Name = sa == null ? String.Empty : sa.Name,
                            };
                resultList = query.ToList();

            }

            return resultList;
        }

        public int Add(Movies record)
        {
            int result = 0;

            using (AppDbContext dbContext = new AppDbContext())
            {
                dbContext.Entry(record).State = EntityState.Added;
                result = dbContext.SaveChanges();
            }

            return result;
        }

        public int Update(Movies record)
        {
            int result = 0;

            using (AppDbContext dbContext = new AppDbContext())
            {
                dbContext.Entry(record).State = EntityState.Modified;
                result = dbContext.SaveChanges();
            }

            return result;
        }

        public int Delete(Movies record)
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
