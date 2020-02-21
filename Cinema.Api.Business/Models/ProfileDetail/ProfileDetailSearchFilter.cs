using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Api.Business.Models.ProfileDetail
{
    public class ProfileDetailSearchFilter
    {
        // paging
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }

        // sorting
        public string SortOn { get; set; }
        public string SortDirection { get; set; }

        // filters        
        public int? Filter_ProfileId { get; set; }
        public int? Filter_AuthId { get; set; }
    }
}
