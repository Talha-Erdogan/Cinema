using Cinema.Api.Business.Models;
using Cinema.Api.Business.Models.Movies;
using Cinema.Api.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Api.Business.Interfaces
{
    public interface IMoviesService
    {
        PaginatedList<MoviesWithDetail> GetAllPaginatedWithDetailBySearchFilter(MoviesSearchFilter searchFilter);
        List<MoviesWithDetail> GetAllWithDetail();
        Movies GetById(int id);
        int Add(Movies record);
        int Update(Movies record);
        int Delete(Movies record);

    }
}
