using Cinema.Api.Business.Models;
using Cinema.Api.Business.Models.Movies;
using Cinema.Api.Business.Models.MoviesType;
using Cinema.Api.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Api.Business.Interfaces
{
    public interface IMoviesTypeService
    {
        PaginatedList<MoviesType> GetAllPaginatedBySearchFilter(MoviesTypeSearchFilter searchFilter);
        List<MoviesType> GetAll();
        int Add(MoviesType record);
        int Update(MoviesType record);
        int Delete(MoviesType record);

    }
}
