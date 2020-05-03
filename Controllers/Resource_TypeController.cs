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
    public class Resource_TypeController : Controller
    {
        private Session1Entities db = new Session1Entities();

        // GET: Resource_Type
        public ActionResult Index()
        {
            return View(db.Resource_Type.ToList());
        }

        // GET: Resource_Type/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Resource_Type resource_Type = db.Resource_Type.Find(id);
            if (resource_Type == null)
            {
                return HttpNotFound();
            }
            return View(resource_Type);
        }

        // GET: Resource_Type/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Resource_Type/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "resTypeId,resTypeName")] Resource_Type resource_Type)
        {
            if (ModelState.IsValid)
            {
                db.Resource_Type.Add(resource_Type);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(resource_Type);
        }

        // GET: Resource_Type/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Resource_Type resource_Type = db.Resource_Type.Find(id);
            if (resource_Type == null)
            {
                return HttpNotFound();
            }
            return View(resource_Type);
        }

        // POST: Resource_Type/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "resTypeId,resTypeName")] Resource_Type resource_Type)
        {
            if (ModelState.IsValid)
            {
                db.Entry(resource_Type).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(resource_Type);
        }

        // GET: Resource_Type/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Resource_Type resource_Type = db.Resource_Type.Find(id);
            if (resource_Type == null)
            {
                return HttpNotFound();
            }
            return View(resource_Type);
        }

        // POST: Resource_Type/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Resource_Type resource_Type = db.Resource_Type.Find(id);
            db.Resource_Type.Remove(resource_Type);
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
