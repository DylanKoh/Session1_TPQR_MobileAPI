using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
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
            var customListView = new List<CustomView>();
            var ResourceList = (from x in db.Resources
                                select x);

            foreach (var item in ResourceList)
            {
                var newCustomView = new CustomView();
                if (item.remainingQuantity > 5)
                {
                    newCustomView.ResourceName = item.resName;
                    newCustomView.ResourceType = db.Resource_Type.Where(x => x.resTypeId == item.resTypeIdFK).Select(x => x.resTypeName).FirstOrDefault();
                    newCustomView.AvailableQuantity = "Sufficient";
                    newCustomView.NumberOfSkills = db.Resource_Allocation.Where(z => z.resIdFK == item.resId).Select(z => z).Count();
                    newCustomView.AllocatedSkills = string.Empty;
                    if (newCustomView.NumberOfSkills == 0)
                    {
                        newCustomView.AllocatedSkills = "Nil";
                    }
                    else
                    {
                        var getAllocatedSkillsList = db.Resource_Allocation.Where(x => x.resIdFK == item.resId).Select(x => x).ToList();
                        foreach (var allocatedSkill in getAllocatedSkillsList)
                        {
                            if (newCustomView.AllocatedSkills == string.Empty)
                            {
                                newCustomView.AllocatedSkills = db.Skills.Where(x => x.skillId == allocatedSkill.skillIdFK).Select(x => x.skillName).FirstOrDefault();
                            }
                            else
                            {
                                newCustomView.AllocatedSkills += $", {db.Skills.Where(x => x.skillId == allocatedSkill.skillIdFK).Select(x => x.skillName).FirstOrDefault()}";
                            }
                        }
                    }

                }
                else if (item.remainingQuantity > 1 && item.remainingQuantity <= 5)
                {
                    newCustomView.ResourceName = item.resName;
                    newCustomView.ResourceType = db.Resource_Type.Where(x => x.resTypeId == item.resTypeIdFK).Select(x => x.resTypeName).FirstOrDefault();
                    newCustomView.AvailableQuantity = "Low Stock";
                    newCustomView.NumberOfSkills = db.Resource_Allocation.Where(z => z.resIdFK == item.resId).Select(z => z).Count();
                    newCustomView.AllocatedSkills = string.Empty;
                    if (newCustomView.NumberOfSkills == 0)
                    {
                        newCustomView.AllocatedSkills = "Nil";
                    }
                    else
                    {
                        var getAllocatedSkillsList = db.Resource_Allocation.Where(x => x.resIdFK == item.resId).Select(x => x).ToList();
                        foreach (var allocatedSkill in getAllocatedSkillsList)
                        {
                            if (newCustomView.AllocatedSkills == string.Empty)
                            {
                                newCustomView.AllocatedSkills = db.Skills.Where(x => x.skillId == allocatedSkill.skillIdFK).Select(x => x.skillName).FirstOrDefault();
                            }
                            else
                            {
                                newCustomView.AllocatedSkills += $", {db.Skills.Where(x => x.skillId == allocatedSkill.skillIdFK).Select(x => x.skillName).FirstOrDefault()}";
                            }
                        }
                    }
                }
                else
                {
                    newCustomView.ResourceName = item.resName;
                    newCustomView.ResourceType = db.Resource_Type.Where(x => x.resTypeId == item.resTypeIdFK).Select(x => x.resTypeName).FirstOrDefault();
                    newCustomView.AvailableQuantity = "Not Available";
                    newCustomView.NumberOfSkills = db.Resource_Allocation.Where(z => z.resIdFK == item.resId).Select(z => z).Count();
                    newCustomView.AllocatedSkills = string.Empty;
                    if (newCustomView.NumberOfSkills == 0)
                    {
                        newCustomView.AllocatedSkills = "Nil";
                    }
                    else
                    {
                        var getAllocatedSkillsList = db.Resource_Allocation.Where(x => x.resIdFK == item.resId).Select(x => x).ToList();
                        foreach (var allocatedSkill in getAllocatedSkillsList)
                        {
                            if (newCustomView.AllocatedSkills == string.Empty)
                            {
                                newCustomView.AllocatedSkills = db.Skills.Where(x => x.skillId == allocatedSkill.skillIdFK).Select(x => x.skillName).FirstOrDefault();
                            }
                            else
                            {
                                newCustomView.AllocatedSkills += $", {db.Skills.Where(x => x.skillId == allocatedSkill.skillIdFK).Select(x => x.skillName).FirstOrDefault()}";
                            }
                        }
                    }
                }

                customListView.Add(newCustomView);
            }
            return new JsonResult { Data = customListView };

        }

        // POST: Resources/Details/5
        [HttpPost]
        public ActionResult Details(string ResourceName)
        {
           
            Resource resource = db.Resources.Where(x => x.resName == ResourceName).Select(x => x).FirstOrDefault();
            return Json(resource);
        }


        // POST: Resources/Create
        [HttpPost]
        public ActionResult Create([Bind(Include = "resId,resName,resTypeIdFK,remainingQuantity")] Resource resource)
        {
            var checkResource = db.Resources.Where(x => x.resName == resource.resName).Select(x => x).FirstOrDefault();
            if (checkResource == null)
            {
                if (ModelState.IsValid)
                {
                    db.Resources.Add(resource);
                    db.SaveChanges();
                    return Json("Resource Created successfully!");
                }
                return Json("Unable to create resource!");
            }
            else
            {
                return Json("Resource Name already exist!");
            }



        }


        // POST: Resources/Edit/5
        [HttpPost]
        public ActionResult Edit([Bind(Include = "resId,resName,resTypeIdFK,remainingQuantity")] Resource resource)
        {
            if (ModelState.IsValid)
            {
                db.Entry(resource).State = EntityState.Modified;
                db.SaveChanges();
                return Json("Edit resource successful!");
            }
            return Json("Unable to edit resource!");
        }

        // POST: Resources/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(string ResourceName)
        {
            Resource resource = db.Resources.Where(x => x.resName == ResourceName).Select(x => x).FirstOrDefault();
            var allocation = db.Resource_Allocation.Where(x => x.resIdFK == resource.resId).Select(x => x);
            foreach (var item in allocation)
            {
                db.Resource_Allocation.Remove(item);
            }
            db.Resources.Remove(resource);
            db.SaveChanges();
            
            return Json("Resource has been successfully deleted!");
        }

        public ActionResult GetLastestID()
        {
            var newID = db.Resources.OrderByDescending(x => x.resId).Select(x => x.resId).FirstOrDefault();
            return Json(newID);
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
    public class CustomView
    {
        public string ResourceName { get; set; }
        public string ResourceType { get; set; }
        public int NumberOfSkills { get; set; }
        public string AllocatedSkills { get; set; }
        public string AvailableQuantity { get; set; }
    }

}
