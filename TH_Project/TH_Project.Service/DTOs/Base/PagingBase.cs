using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TH_Project.Service.DTOs.Base
{
    public class PagingResultBase
    {
        public int TotalPages { get; set; }

        public int PageSize { get; set; }
        public int PageNumber { get; set; }

        public int TotalRecords { get; set; }
        public int PageCount
        {
            get
            {
                var pageCount = (double)TotalRecords / PageSize;
                return (int)Math.Ceiling(pageCount);
            }
        }

    }
    public class PagedResult<T> : PagingResultBase
    {
        public List<T> Items { get; set; }
    }

    public class PagingBaseRequest
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }

    }
}
