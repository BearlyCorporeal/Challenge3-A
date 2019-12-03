using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Challenge3_a_API.Models
{
    public class Borrowerview
    {
        public int id { get; set; }
        public string Surname { get; set; }
        public string Firstname { get; set; }
        public string DOB { get; set; }
        public Borrowerview(int id,string Surname,string Firstname,string DOB)
        {
            this.id = id;
            this.Surname = Surname;
            this.Firstname = Firstname;
            this.DOB = DOB;
        }
    }
}