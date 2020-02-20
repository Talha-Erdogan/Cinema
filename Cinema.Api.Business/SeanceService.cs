using Cinema.Api.Business.Models;
using Cinema.Api.Business.Models.Seance;
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
    public class SeanceService: ISeanceService
    {
        public PaginatedList<Seance> GetAllPaginatedBySearchFilter(SeanceSearchFilter searchFilter)
        {
            PaginatedList<Seance> resultList = new PaginatedList<Seance>(new List<Seance>(), 0, searchFilter.CurrentPage, searchFilter.PageSize, searchFilter.SortOn, searchFilter.SortDirection);

            using (AppDbContext dbContext = new AppDbContext())
            {

                var query = from sa in dbContext.Seance.Where(sa => sa.IsDeleted == false)
                            where sa.IsDeleted == false
                            select new Seance()
                            {
                                Id = sa.Id,
                                Name = sa.Name,
                                Date = sa.Date,
                                Time =sa.Time,
                                IsDeleted = sa.IsDeleted,
                            };

                // filtering

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


                resultList = new PaginatedList<Seance>(
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

        public List<Seance> GetAll()
        {
            List<Seance> resultList = new List<Seance>();

            using (AppDbContext dbContext = new AppDbContext())
            {
                var query = from sa in dbContext.Seance.Where(sa => sa.IsDeleted == false)
                            where sa.IsDeleted == false
                            select new Seance()
                            {
                                Id = sa.Id,
                                Name = sa.Name,
                                Date = sa.Date,
                                Time = sa.Time,
                                IsDeleted = sa.IsDeleted,
                            };
                resultList = query.ToList();

            }

            return resultList;
        }

        public Seance GetById(int id)
        {
            Seance result = new Seance();

            using (AppDbContext dbContext = new AppDbContext())
            {
                result = dbContext.Seance.Where(r => r.Id == id && r.IsDeleted == false).AsNoTracking().SingleOrDefault();
            }

            return result;
        }

        public int Add(Seance record)
        {
            int result = 0;

            using (AppDbContext dbContext = new AppDbContext())
            {
                dbContext.Entry(record).State = EntityState.Added;
                result = dbContext.SaveChanges();
            }

            return result;
        }

        public int Update(Seance record)
        {
            int result = 0;

            using (AppDbContext dbContext = new AppDbContext())
            {
                dbContext.Entry(record).State = EntityState.Modified;
                result = dbContext.SaveChanges();
            }

            return result;
        }

        public int Delete(Seance record)
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
