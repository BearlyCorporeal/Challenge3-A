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
    public class BorrowersController : ApiController
    {
        private civapiEntities db = new civapiEntities();

        // GET: api/Borrowers
        public List<Borrowerview> GetBorrowers()
        {
            List<Borrower> list = (from a in db.Borrowers select a).ToList();
            List<Borrowerview> newlist = new List<Borrowerview>();
            foreach (Borrower b in list)
            {
                int newid = b.id;
                string newFirstname = b.Firstname;
                string newSurname = b.Surname;
                string newDOB = b.DOB;
                newlist.Add(new Borrowerview(newid, newFirstname,newSurname,newDOB));
            }
            return newlist;
        }

        // GET: api/Borrowers/5
        [ResponseType(typeof(Borrower))]
        public IHttpActionResult GetBorrower(int id)
        {
            Borrower borrower = db.Borrowers.Find(id);
            if (borrower == null)
            {
                return NotFound();
            }

            return Ok(borrower);
        }

        // PUT: api/Borrowers/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutBorrower(int id, Borrower borrower)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != borrower.id)
            {
                return BadRequest();
            }

            db.Entry(borrower).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BorrowerExists(id))
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

        // POST: api/Borrowers
        [ResponseType(typeof(Borrower))]
        public IHttpActionResult PostBorrower(Borrower borrower)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Borrowers.Add(borrower);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (BorrowerExists(borrower.id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = borrower.id }, borrower);
        }

        // DELETE: api/Borrowers/5
        [ResponseType(typeof(Borrower))]
        public IHttpActionResult DeleteBorrower(int id)
        {
            Borrower borrower = db.Borrowers.Find(id);
            if (borrower == null)
            {
                return NotFound();
            }

            db.Borrowers.Remove(borrower);
            db.SaveChanges();

            return Ok(borrower);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool BorrowerExists(int id)
        {
            return db.Borrowers.Count(e => e.id == id) > 0;
        }
    }
}