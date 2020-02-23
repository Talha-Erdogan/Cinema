using Cinema.Web.Business.Models;
using Cinema.Web.Business.Models.Seance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Web.Business.Interfaces
{
    public interface ISeanceService
    {
        ApiResponseModel<PaginatedList<Seance>> GetAllPaginatedWithDetailBySearchFilter(string userToken, SeanceSearchFilter searchFilter);
        ApiResponseModel<List<Seance>> GetAll(string userToken);
        ApiResponseModel<Seance> GetById(string userToken, int id);
        ApiResponseModel<Seance> Add(string userToken, Seance seance);
        ApiResponseModel<Seance> Edit(string userToken, Seance seance);
        ApiResponseModel<Seance> Delete(string userToken, int seanceId);

    }
}
