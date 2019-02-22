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
    public class ProductsController : ApiController
    {
        private store_modelEntities db = new store_modelEntities();

        // GET: api/Products
        public IQueryable<product> Getproduct()
        {
            return db.product;
        }

        // GET: api/Products/5
        [ResponseType(typeof(product))]
        public IHttpActionResult Getproduct(decimal id)
        {
            product product = db.product.Find(id);
            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        // PUT: api/Products/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putproduct(decimal id, product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != product.id_product)
            {
                return BadRequest();
            }

            db.Entry(product).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!productExists(id))
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

        // POST: api/Products
        [ResponseType(typeof(product))]
        public IHttpActionResult Postproduct(product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.product.Add(product);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = product.id_product }, product);
        }

        // DELETE: api/Products/5
        [ResponseType(typeof(product))]
        public IHttpActionResult Deleteproduct(decimal id)
        {
            product product = db.product.Find(id);
            if (product == null)
            {
                return NotFound();
            }

            db.product.Remove(product);
            db.SaveChanges();

            return Ok(product);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool productExists(decimal id)
        {
            return db.product.Count(e => e.id_product == id) > 0;
        }
    }
}