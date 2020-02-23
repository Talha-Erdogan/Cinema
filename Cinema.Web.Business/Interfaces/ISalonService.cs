using Cinema.Web.Business.Models;
using Cinema.Web.Business.Models.Salon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Web.Business.Interfaces
{
    public interface ISalonService
    {
        ApiResponseModel<PaginatedList<Salon>> GetAllPaginatedWithDetailBySearchFilter(string userToken, SalonSearchFilter searchFilter);
        ApiResponseModel<List<Salon>> GetAll(string userToken);
        ApiResponseModel<Salon> GetById(string userToken, int id);
        ApiResponseModel<Salon> Add(string userToken, Salon salon);
        ApiResponseModel<Salon> Edit(string userToken, Salon salon);
        ApiResponseModel<Salon> Delete(string userToken, int salonId);

    }
}
