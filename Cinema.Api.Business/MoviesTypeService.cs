using Cinema.Api.Business.Interfaces;
using Cinema.Api.Business.Models;
using Cinema.Api.Business.Models.MoviesType;
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
    public class MoviesTypeService: IMoviesTypeService
    {
        public PaginatedList<MoviesType> GetAllPaginatedBySearchFilter(MoviesTypeSearchFilter searchFilter)
        {
            PaginatedList<MoviesType> resultList = new PaginatedList<MoviesType>(new List<MoviesType>(), 0, searchFilter.CurrentPage, searchFilter.PageSize, searchFilter.SortOn, searchFilter.SortDirection);

            using (AppDbContext dbContext = new AppDbContext())
            {

                var query = from mt in dbContext.MoviesType.Where(mt => mt.IsDeleted == false)
                            where mt.IsDeleted == false
                            select new MoviesType()
                            {
                                Id = mt.Id,
                                Name = mt.Name,
                                IsDeleted = mt.IsDeleted,
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

                resultList = new PaginatedList<MoviesType>(
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

        public List<MoviesType> GetAll()
        {
            List<MoviesType> resultList = new List<MoviesType>();

            using (AppDbContext dbContext = new AppDbContext())
            {
                var query = from mt in dbContext.MoviesType.Where(mt => mt.IsDeleted == false)
                            where mt.IsDeleted == false
                            select new MoviesType()
                            {
                                Id = mt.Id,
                                Name = mt.Name,
                                IsDeleted = mt.IsDeleted,
                            };
                resultList = query.ToList();
            }

            return resultList;
        }

        public MoviesType GetById(int id)
        {
            MoviesType result = new MoviesType();

            using (AppDbContext dbContext = new AppDbContext())
            {
                result = dbContext.MoviesType.Where(r => r.Id == id && r.IsDeleted == false).AsNoTracking().SingleOrDefault();
            }

            return result;
        }

        public int Add(MoviesType record)
        {
            int result = 0;

            using (AppDbContext dbContext = new AppDbContext())
            {
                dbContext.Entry(record).State = EntityState.Added;
                result = dbContext.SaveChanges();
            }

            return result;
        }

        public int Update(MoviesType record)
        {
            int result = 0;

            using (AppDbContext dbContext = new AppDbContext())
            {
                dbContext.Entry(record).State = EntityState.Modified;
                result = dbContext.SaveChanges();
            }

            return result;
        }

        public int Delete(MoviesType record)
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
