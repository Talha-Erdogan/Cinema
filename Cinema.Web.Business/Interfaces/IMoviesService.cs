using Cinema.Web.Business.Models;
using Cinema.Web.Business.Models.Movies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Web.Business.Interfaces
{
    public interface IMoviesService
    {
        ApiResponseModel<PaginatedList<MoviesWithDetail>> GetAllPaginatedWithDetailBySearchFilter(string userToken, MoviesSearchFilter searchFilter);
        ApiResponseModel<List<Movies>> GetAll(string userToken);
        ApiResponseModel<Movies> GetById(string userToken, int id);
        ApiResponseModel<Movies> Add(string userToken, Movies movies);
        ApiResponseModel<Movies> Edit(string userToken, Movies movies);
        ApiResponseModel<Movies> Delete(string userToken, int moviesId);
    }
}
