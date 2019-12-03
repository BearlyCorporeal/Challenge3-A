using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Challenge3_a_API.Models
{
    public class Bookview
    {
        public int ISBN { get; set; }
        public string title { get; set; }
        public Bookview(int ISBN,string title)
        {
            this.ISBN = ISBN;
            this.title = title;
        }
    }
}