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
    public class User_TypeController : Controller
    {
        private Session1Entities db = new Session1Entities();

        public User_TypeController()
        {
            db.Configuration.LazyLoadingEnabled = false;
        }

        [HttpPost]
        // POST: User_Type
        public ActionResult Index()
        {
            return new JsonResult { Data = db.User_Type.ToList() };
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
