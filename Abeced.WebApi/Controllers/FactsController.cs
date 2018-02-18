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
using Abeced.WebApi.Models.Abeced.Data;

namespace Abeced.WebApi.Controllers
{
    public class FactsController : ApiController
    {
        private AbecedEntities db = new AbecedEntities();

        // GET: api/Facts
        public IQueryable<Fact> GetFacts()
        {
            return db.Facts;
        }

        // GET: api/Facts/5
        [ResponseType(typeof(Fact))]
        public IHttpActionResult GetFact(int id)
        {
            Fact fact = db.Facts.Find(id);
            if (fact == null)
            {
                return NotFound();
            }

            return Ok(fact);
        }

        // PUT: api/Facts/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutFact(int id, Fact fact)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != fact.FactId)
            {
                return BadRequest();
            }

            db.Entry(fact).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FactExists(id))
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

        // POST: api/Facts
        [ResponseType(typeof(Fact))]
        public IHttpActionResult PostFact(Fact fact)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Facts.Add(fact);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = fact.FactId }, fact);
        }

        // DELETE: api/Facts/5
        [ResponseType(typeof(Fact))]
        public IHttpActionResult DeleteFact(int id)
        {
            Fact fact = db.Facts.Find(id);
            if (fact == null)
            {
                return NotFound();
            }

            db.Facts.Remove(fact);
            db.SaveChanges();

            return Ok(fact);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool FactExists(int id)
        {
            return db.Facts.Count(e => e.FactId == id) > 0;
        }
    }
}