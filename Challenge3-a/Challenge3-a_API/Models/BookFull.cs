using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Challenge3_a_API.Models
{
    public class BookFull
    {
        public int ISBN { get; set; }
        public string title { get; set; }
        public Borrower borrower { get; set; }
        public BookFull(int ISBN, string title)
        {
            this.ISBN = ISBN;
            this.title = title;
            borrower = new Borrower();
        }
    }
}