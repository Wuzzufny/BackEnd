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
using WuzzufnyAPI.Models;

namespace WuzzufnyAPI.Controllers
{
    public class AppVersionsController : ApiController
    {
        private WuzzufnyDbEntities db = new WuzzufnyDbEntities();

        // GET: api/AppVersions
        public IQueryable<AppVersion> GetAppVersion()
        {
            return db.AppVersion;
        }

        // GET: api/AppVersions/5
        [ResponseType(typeof(AppVersion))]
        public IHttpActionResult GetAppVersion(int id)
        {
            AppVersion appVersion = db.AppVersion.Find(id);
            if (appVersion == null)
            {
                return NotFound();
            }

            return Ok(appVersion);
        }

        // PUT: api/AppVersions/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutAppVersion(int id, AppVersion appVersion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != appVersion.Id)
            {
                return BadRequest();
            }

            db.Entry(appVersion).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AppVersionExists(id))
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

        // POST: api/AppVersions
        [ResponseType(typeof(AppVersion))]
        public IHttpActionResult PostAppVersion(AppVersion appVersion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.AppVersion.Add(appVersion);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = appVersion.Id }, appVersion);
        }

        // DELETE: api/AppVersions/5
        [ResponseType(typeof(AppVersion))]
        public IHttpActionResult DeleteAppVersion(int id)
        {
            AppVersion appVersion = db.AppVersion.Find(id);
            if (appVersion == null)
            {
                return NotFound();
            }

            db.AppVersion.Remove(appVersion);
            db.SaveChanges();

            return Ok(appVersion);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AppVersionExists(int id)
        {
            return db.AppVersion.Count(e => e.Id == id) > 0;
        }
    }
}