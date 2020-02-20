using Cinema.Api.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cinema.Api.Models
{
    public class ListBaseResponseModel<TData, TFilter> : BaseResponseModel
    {

        public int? CurrentPage { get; set; }
        public int? PageSize { get; set; }
        public string SortOn { get; set; }
        public string SortDirection { get; set; }

        public TFilter Filter { get; set; }

        public PaginatedList<TData> DataList { get; set; }

    }
}