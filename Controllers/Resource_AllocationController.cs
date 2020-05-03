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
    public class Resource_AllocationController : Controller
    {
        private Session1Entities db = new Session1Entities();

        public Resource_AllocationController()
        {
            db.Configuration.LazyLoadingEnabled = false;
        }

        // POST: Resource_Allocation
        [HttpPost]
        public ActionResult Index()
        {
            var resource_Allocation = db.Resource_Allocation.Include(r => r.Resource).Include(r => r.Skill);
            return View(resource_Allocation.ToList());
        }

        // POST: Resource_Allocation/Details/5
        [HttpPost]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Resource_Allocation resource_Allocation = db.Resource_Allocation.Find(id);
            if (resource_Allocation == null)
            {
                return HttpNotFound();
            }
            return View(resource_Allocation);
        }


        // POST: Resource_Allocation/Create
        [HttpPost]
        public ActionResult Create([Bind(Include = "allocId,resIdFK,skillIdFK")] Resource_Allocation resource_Allocation)
        {
            if (ModelState.IsValid)
            {
                db.Resource_Allocation.Add(resource_Allocation);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.resIdFK = new SelectList(db.Resources, "resId", "resName", resource_Allocation.resIdFK);
            ViewBag.skillIdFK = new SelectList(db.Skills, "skillId", "skillName", resource_Allocation.skillIdFK);
            return View(resource_Allocation);
        }

        

        // POST: Resource_Allocation/Edit/5
        [HttpPost]
        public ActionResult Edit([Bind(Include = "allocId,resIdFK,skillIdFK")] Resource_Allocation resource_Allocation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(resource_Allocation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.resIdFK = new SelectList(db.Resources, "resId", "resName", resource_Allocation.resIdFK);
            ViewBag.skillIdFK = new SelectList(db.Skills, "skillId", "skillName", resource_Allocation.skillIdFK);
            return View(resource_Allocation);
        }

       

        // POST: Resource_Allocation/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Resource_Allocation resource_Allocation = db.Resource_Allocation.Find(id);
            db.Resource_Allocation.Remove(resource_Allocation);
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
