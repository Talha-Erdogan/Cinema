using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Web.Business.Models
{
    public class PaginatedList<T>
    {
        public int CurrentPage { get; set; }

        public List<T> Items { get; set; } // mvc view'de post edilen listeler için private set ataması kaldırıldı 16.12.2019 15:30
        public int PageSize { get; set; }

        public int From { get; set; }
        public int To { get; set; }

        public int TotalCount { get; set; }
        public int TotalPages { get; set; }

        public string SortOn { get; set; }
        public string SortDirection { get; set; }

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

        // mvc view'de post edilen listeler için kullanılabilmesi için parametresiz ctor kısmı eklendi 16.12.2019 15:30
        public PaginatedList()
        {

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
