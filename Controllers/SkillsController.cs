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
    public class SkillsController : Controller
    {
        private Session1Entities db = new Session1Entities();

        public SkillsController()
        {
            db.Configuration.LazyLoadingEnabled = false;
        }

        // POST: Skills
        [HttpPost]
        public ActionResult Index()
        {
            return new JsonResult { Data = db.Skills.ToList() };
        }

        // POST: Skills/Details/5
        [HttpPost]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Skill skill = db.Skills.Find(id);
            if (skill == null)
            {
                return HttpNotFound();
            }
            return View(skill);
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
