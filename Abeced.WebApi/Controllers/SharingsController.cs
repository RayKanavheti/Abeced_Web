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
    public class SharingsController : ApiController
    {
        private AbecedEntities db = new AbecedEntities();

        // GET: api/Sharings
        public IQueryable<Sharing> GetSharings()
        {
            return db.Sharings;
        }

        // GET: api/Sharings/5
        [ResponseType(typeof(Sharing))]
        public IHttpActionResult GetSharing(int id)
        {
            Sharing sharing = db.Sharings.Find(id);
            if (sharing == null)
            {
                return NotFound();
            }

            return Ok(sharing);
        }

        // PUT: api/Sharings/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutSharing(int id, Sharing sharing)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != sharing.ShareId)
            {
                return BadRequest();
            }

            db.Entry(sharing).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SharingExists(id))
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

        // POST: api/Sharings
        [ResponseType(typeof(Sharing))]
        public IHttpActionResult PostSharing(Sharing sharing)
        {
           
            db.Sharings.Add(sharing);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = sharing.ShareId }, sharing);
        }

        // DELETE: api/Sharings/5
        [ResponseType(typeof(Sharing))]
        public IHttpActionResult DeleteSharing(int id)
        {
            Sharing sharing = db.Sharings.Find(id);
            if (sharing == null)
            {
                return NotFound();
            }

            db.Sharings.Remove(sharing);
            db.SaveChanges();

            return Ok(sharing);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SharingExists(int id)
        {
            return db.Sharings.Count(e => e.ShareId == id) > 0;
        }
    }
}