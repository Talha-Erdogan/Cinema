using Cinema.Web.Business.Models;
using Cinema.Web.Business.Models.MoviesType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Web.Business.Interfaces
{
    public interface IMoviesTypeService
    {
        ApiResponseModel<PaginatedList<MoviesType>> GetAllPaginatedWithDetailBySearchFilter(string userToken, MoviesTypeSearchFilter searchFilter);
        ApiResponseModel<List<MoviesType>> GetAll(string userToken);
        ApiResponseModel<MoviesType> GetById(string userToken, int id);
        ApiResponseModel<MoviesType> Add(string userToken, MoviesType moviesType);
        ApiResponseModel<MoviesType> Edit(string userToken, MoviesType moviesType);
        ApiResponseModel<MoviesType> Delete(string userToken, int moviesTypeId);

    }
}
