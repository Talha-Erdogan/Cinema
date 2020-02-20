using Cinema.Api.Business.Models;
using Cinema.Api.Business.Models.Seance;
using Cinema.Api.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Api.Business.Interfaces
{
    public interface ISeanceService
    {
        PaginatedList<Seance> GetAllPaginatedBySearchFilter(SeanceSearchFilter searchFilter);
        List<Seance> GetAll();
        Seance GetById(int id);
        int Add(Seance record);
        int Update(Seance record);
        int Delete(Seance record);
    }
}
