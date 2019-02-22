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
using store_backend_apirest;

namespace store_backend_apirest.Controllers
{
    public class storesController : ApiController
    {
        private store_modelEntities db = new store_modelEntities();

        // GET: api/stores
        public IQueryable<store> Getstore()
        {
            return db.store;
        }

        // GET: api/stores/5
        [ResponseType(typeof(store))]
        public IHttpActionResult Getstore(decimal id)
        {
            store store = db.store.Find(id);
            if (store == null)
            {
                return NotFound();
            }

            return Ok(store);
        }

        // PUT: api/stores/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putstore(decimal id, store store)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != store.id_store)
            {
                return BadRequest();
            }

            db.Entry(store).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!storeExists(id))
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

        // POST: api/stores
        [ResponseType(typeof(store))]
        public IHttpActionResult Poststore(store store)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.store.Add(store);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = store.id_store }, store);
        }

        // DELETE: api/stores/5
        [ResponseType(typeof(store))]
        public IHttpActionResult Deletestore(decimal id)
        {
            store store = db.store.Find(id);
            if (store == null)
            {
                return NotFound();
            }

            db.store.Remove(store);
            db.SaveChanges();

            return Ok(store);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool storeExists(decimal id)
        {
            return db.store.Count(e => e.id_store == id) > 0;
        }
    }
}