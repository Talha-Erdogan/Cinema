﻿using Cinema.Api.Business.Models;
using Cinema.Api.Business.Models.Profile;
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
    public class ProfileService : IProfileService
    {
        public PaginatedList<Profile> GetAllPaginatedBySearchFilter(ProfileSearchFilter searchFilter)
        {
            PaginatedList<Profile> resultList = new PaginatedList<Profile>(new List<Profile>(), 0, searchFilter.CurrentPage, searchFilter.PageSize, searchFilter.SortOn, searchFilter.SortDirection);

            using (AppDbContext dbContext = new AppDbContext())
            {
                var query = dbContext.Profile.Where(p => p.IsDeleted == false).AsNoTracking();

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


                resultList = new PaginatedList<Profile>(
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

        public List<Profile> GetAll()
        {
            List<Profile> resultList = new List<Profile>();

            using (AppDbContext dbContext = new AppDbContext())
            {
                var query = dbContext.Profile.Where(p => p.IsDeleted == false).AsNoTracking();
                resultList = query.ToList();
            }

            return resultList;
        }

        public Profile GetById(int id)
        {
            Profile result = new Profile();

            using (AppDbContext dbContext = new AppDbContext())
            {
                result = dbContext.Profile.Where(r => r.Id == id && r.IsDeleted == false).AsNoTracking().SingleOrDefault();
            }

            return result;
        }

        public int Add(Profile record)
        {
            int result = 0;

            using (AppDbContext dbContext = new AppDbContext())
            {
                dbContext.Entry(record).State = EntityState.Added;
                result = dbContext.SaveChanges();
            }

            return result;
        }

        public int Update(Profile record)
        {
            int result = 0;

            using (AppDbContext dbContext = new AppDbContext())
            {
                dbContext.Entry(record).State = EntityState.Modified;
                result = dbContext.SaveChanges();
            }

            return result;
        }

        public int Delete(Profile record)
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
