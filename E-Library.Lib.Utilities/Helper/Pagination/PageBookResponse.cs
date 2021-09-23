using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Library.Lib.Utilities.Helper.Pagination
{
    public class PageBookResponse<T>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        public int TotalRecords { get; set; }
        public IEnumerable<T> Data { get; private set; }
        public bool Success { get; set; }
        public string[] Errors { get; set; }
        public string Message { get; set; }

        private PageBookResponse(bool success, string message, IEnumerable<T> data)
        {
            Data = data;
        }

        //Success response
        public PageBookResponse(IEnumerable<T> data, int pageNumber, int pageSize) : this(true, string.Empty, data)
        {
            this.PageNumber = pageNumber;
            this.PageSize = pageSize;
            this.Data = data;
            this.Message = null;
            this.Success = true;
            this.Errors = null;
        }

        //Failed response
        public PageBookResponse(string message) : this(false, message, null) { }
    }
}
