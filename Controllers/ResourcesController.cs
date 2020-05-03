using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Session1_TPQR_MobileAPI;

namespace Session1_TPQR_MobileAPI.Controllers
{
    public class ResourcesController : Controller
    {
        private Session1Entities db = new Session1Entities();

        public ResourcesController()
        {
            db.Configuration.LazyLoadingEnabled = false;
        }

        // POST: Resources
        [HttpPost]
        public ActionResult Index()
        {
            var resources = db.Resources;
            return new JsonResult { Data = resources.ToList() };
        }

        // POST: Resources/Details/5
        [HttpPost]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Resource resource = db.Resources.Find(id);
            if (resource == null)
            {
                return HttpNotFound();
            }
            return View(resource);
        }


        // POST: Resources/Create
        [HttpPost]
        public ActionResult Create([Bind(Include = "resId,resName,resTypeIdFK,remainingQuantity")] Resource resource)
        {
            if (ModelState.IsValid)
            {
                db.Resources.Add(resource);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.resTypeIdFK = new SelectList(db.Resource_Type, "resTypeId", "resTypeName", resource.resTypeIdFK);
            return View(resource);
        }


        // POST: Resources/Edit/5
        [HttpPost]
        public ActionResult Edit([Bind(Include = "resId,resName,resTypeIdFK,remainingQuantity")] Resource resource)
        {
            if (ModelState.IsValid)
            {
                db.Entry(resource).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.resTypeIdFK = new SelectList(db.Resource_Type, "resTypeId", "resTypeName", resource.resTypeIdFK);
            return View(resource);
        }

        // POST: Resources/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Resource resource = db.Resources.Find(id);
            db.Resources.Remove(resource);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
