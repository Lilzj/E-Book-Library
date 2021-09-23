using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace E_Library.Lib.Utilities.Helper.Pagination
{
    public class PaginationFilter<T>
    {
        //public int PageNumber { get; set; } = 1;
        public int CurrentPage { get; set; }
        public int PageSize { get; set; } = 18;
        public int Totalpages  { get; set; }
        public int TotalRecords { get; set; }
        public bool PreviousPage => CurrentPage > 1;
        public bool NextPage => CurrentPage < Totalpages;
        public List<T> Items { get; set; } = new List<T>();


        public PaginationFilter(List<T> items, int count, int pageNumber, int pagesize)
        {
            TotalRecords = count;
            PageSize = pagesize;
            CurrentPage = pageNumber;
            Totalpages = (int)Math.Ceiling(count / (double)PageSize);

            Items.AddRange(items);
        }

        public static PaginationFilter<T> PaginationSet(List<T> items, int pageNumber, int pageSize)
        {
            var count = items.Count;
            var pageResult = items.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

            return new PaginationFilter<T>(pageResult,count,pageNumber,pageSize);
        }
    }
}
