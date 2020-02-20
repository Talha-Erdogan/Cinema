using Cinema.Api.Business.Models;
using Cinema.Api.Business.Models.Salon;
using Cinema.Api.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Api.Business.Interfaces
{
    public interface ISalonService
    {
        PaginatedList<Salon> GetAllPaginatedBySearchFilter(SalonSearchFilter searchFilter);
        List<Salon> GetAll();
        int Add(Salon record);
        int Update(Salon record);
        int Delete(Salon record);
    }
}
