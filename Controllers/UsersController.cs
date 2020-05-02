using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using Session1_TPQR_MobileAPI;

namespace Session1_TPQR_MobileAPI.Controllers
{
    public class UsersController : Controller
    {
        private Session1Entities db = new Session1Entities();

        public UsersController()
        {
            db.Configuration.LazyLoadingEnabled = false;
        }

        // GET: Users/Details/{userid}
        // TO EDIT IF NEEDED
        [HttpPost]
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return Json(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Where(x=> x.userId == id).Select(x => x).FirstOrDefault();
            if (user == null)
            {
                return Json(HttpStatusCode.BadRequest);
            }
            return Json(user);
        }


        // POST: Users/Create
        [HttpPost]
        public ActionResult Create([Bind(Include = "userId,userName,userPw,userTypeIdFK")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Users.Add(user);
                db.SaveChanges();
                return Json("User has been created!");
            }

            return Json("Error, User cannot be created. Please check your fields and try again!");
        }


        // POST: Users/Edit/5
        [HttpPost]
        public ActionResult Edit([Bind(Include = "userId,userName,userPw,userTypeIdFK")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.userTypeIdFK = new SelectList(db.User_Type, "userTypeId", "userTypeName", user.userTypeIdFK);
            return View(user);
        }


        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(string id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // POST: Users/Login?username={username}&password={password}
        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            var user = (from x in db.Users
                        where x.userId == username && x.userPw == password
                        select x).FirstOrDefault();
            if (user != null)
            {
                return Json(user);
            }

            return Json("User does not exist. Please try again!");
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
