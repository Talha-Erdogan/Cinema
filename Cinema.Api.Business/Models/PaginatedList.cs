using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Api.Business.Models
{
    public class PaginatedList<T>
    {
        public int CurrentPage { get; private set; }

        public List<T> Items { get; private set; }
        public int PageSize { get; private set; }

        public int From { get; private set; }
        public int To { get; private set; }

        public int TotalCount { get; private set; }
        public int TotalPages { get; private set; }

        public string SortOn { get; private set; }
        public string SortDirection { get; private set; }

        public PaginatedList(List<T> items, int totalCount, int currentPage, int pageSize, string sortOn, string sortDirection)
        {
            CurrentPage = currentPage;
            TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
            TotalCount = totalCount;
            PageSize = pageSize;
            From = ((currentPage - 1) * pageSize) + 1;
            To = (From + pageSize) - 1;

            SortOn = sortOn;
            SortDirection = sortDirection;

            Items = items;
        }

        public bool HasPreviousPage
        {
            get
            {
                return (CurrentPage > 1);
            }
        }

        public bool HasNextPage
        {
            get
            {
                return (CurrentPage < TotalPages);
            }
        }

    }
}
