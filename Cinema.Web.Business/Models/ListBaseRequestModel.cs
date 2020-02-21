using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Web.Business.Models
{
    public class ListBaseRequestModel<TFilter> : BaseRequestModel
    {
        public int? CurrentPage { get; set; }
        public int? PageSize { get; set; }
        public string SortOn { get; set; }
        public string SortDirection { get; set; }

        public TFilter Filter { get; set; }

    }
}
