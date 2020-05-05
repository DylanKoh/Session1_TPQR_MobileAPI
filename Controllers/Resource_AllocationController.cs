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
            var resource_Allocation = db.Resource_Allocation;
            return Json(resource_Allocation.ToList());
        }

        // POST: Resource_Allocation/GetDetails?resID{}
        [HttpPost]
        public ActionResult GetDetails(string resID)
        {
            var _resID = Int32.Parse(resID);
            var resource_Allocation = db.Resource_Allocation.Where(x => x.resIdFK == _resID).Select(x => x);
            return new JsonResult { Data = resource_Allocation.ToList() };
        }


        // POST: Resource_Allocation/Create
        [HttpPost]
        public ActionResult Create([Bind(Include = "allocId,resIdFK,skillIdFK")] Resource_Allocation resource_Allocation)
        {
            if (ModelState.IsValid)
            {
                db.Resource_Allocation.Add(resource_Allocation);
                db.SaveChanges();
                return Json("Allocation created successfully!");
            }

            return Json("Unable to create allocation!");
        }

        // POST: Resource_Allocation/Delete?ResID={}
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(string ResID)
        {
            var _resID = Int32.Parse(ResID);
            var toDelete = db.Resource_Allocation.Where(x => x.resIdFK == _resID).Select(x => x);
            foreach (var item in toDelete)
            {
                db.Resource_Allocation.Remove(item);
            }
            db.SaveChanges();
            return Json("Delete successful!");
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
