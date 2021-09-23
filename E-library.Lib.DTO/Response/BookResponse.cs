using E_library.Lib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_library.Lib.DTO.Response
{
    public class BookResponse
    {
        public bool Success { get; protected set; }
        public string Message { get; protected set; }
        public Book Book { get; protected set; }

        public BookResponse(bool success, string message, Book book)
        {
            Success = success;
            Message = message;
            Book = book;
        }

        //To create a success reponse
        public BookResponse(Book book) : this(true, string.Empty, book) { }

        //To create a failed response
        public BookResponse(string message) : this(false, message, null) { }


    }
}
