using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Challenge3_a_API.Models;

namespace Challenge3_a_API.Controllers
{
    public class BooksController : ApiController
    {
        private civapiEntities db = new civapiEntities();

        // GET: api/Books
        public List<Bookview> GetBooks()
        {
            List<Book> list = (from a in db.Books select a ).ToList();
            List<Bookview> newlist = new List<Bookview>();
            foreach(Book b in list)
            {
                int newISBN = b.ISBN;
                string newtitle = b.title;
                newlist.Add(new Bookview(newISBN,newtitle));
            }
            return newlist;
        }

        // GET: api/Books/5
        [ResponseType(typeof(Book))]
        public List<Bookview> GetBook(string id)
        {
            List<Book> list = (from a in db.Books select a).ToList();
            List<Bookview> newlist = new List<Bookview>();
            if (id == "NotBorrowed")
            {
                list = (from s in db.Books where s.borrower == null select s).ToList();
                foreach (Book b in list)
                {
                    int newISBN = b.ISBN;
                    string newtitle = b.title;
                    
                    newlist.Add(new Bookview(newISBN, newtitle));
                }
            }
            
            return newlist;
        }

        // GET: api/Books
        [HttpGet]
        public List<Book> Borrowed()
        {
            List<Book> list = (from a in db.Books select a).ToList();
            list = (from s in db.Books where s.borrower != null select s).ToList();
            return list;
        }


        // PUT: api/Books/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutBook(int id, Book book)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != book.ISBN)
            {
                return BadRequest();
            }

            db.Entry(book).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Books
        [ResponseType(typeof(Book))]
        public IHttpActionResult PostBook(Book book)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Books.Add(book);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (BookExists(book.ISBN))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = book.ISBN }, book);
        }

        // DELETE: api/Books/5
        [ResponseType(typeof(Book))]
        public IHttpActionResult DeleteBook(int id)
        {
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return NotFound();
            }

            db.Books.Remove(book);
            db.SaveChanges();

            return Ok(book);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool BookExists(int id)
        {
            return db.Books.Count(e => e.ISBN == id) > 0;
        }
    }
}