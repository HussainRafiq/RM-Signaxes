using RM_Signaxes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RM_Signaxes.Areas.Admin.Controllers
{
    [AdminSessionAuthorize]
    public class JobController : Controller
    {
        RM_Model db = new RM_Model();
        // GET: Admin/Job
        public ActionResult Index()
        {
            var jobs = db.Jobs.Where(x =>  x.IsDelete == false);
            return View(jobs);
        }


        public ActionResult Create(int? ID = null)
        {
            if (ID != null)
            {
                var jobs = db.Jobs.Where(x => x.Id == ID && x.IsDelete == false);
                if (jobs.Any())
                {
                    return View(jobs.ToList()[0]);
                }
                else
                {
                    TempData["Error"] = "job not found.";
                }
            }
            else
            {
                return View();
            }
            return RedirectToAction("index");
        }
        [HttpPost]
        public ActionResult Create(Job job)
        {
            
            if (job.Id == null || job.Id == 0)
            {             

                int adminUserID = GetAdminUserID();
                job.AddedByID = adminUserID;
                job.IsDelete = false;
            }
            try
            {
                if (ModelState.IsValid)
                {
                    if (job.Id == null || job.Id == 0)
                    {
                        db.Jobs.Add(job);
                        db.SaveChanges();
                        TempData["Success"] = "Job created successfully";
                    }
                    else
                    {
                        var jobobj = db.Jobs.Find(job.Id);
                        if (jobobj != null && jobobj.IsDelete == false )
                        {
                            jobobj.Description = job.Description;
                            jobobj.Title = job.Title;                           

                            db.SaveChanges();
                            TempData["Success"] = "Job updated successfully";
                        }
                    }

                    return RedirectToAction("index");

                }
                else
                {
                    TempData["Error"] = "Model No Valid";
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Some Error Occurred";
            }

            return View();
        }
        public ActionResult Delete(int ID)
        {
            var jobobj = db.Jobs.Find(ID);
            if (jobobj != null && jobobj.IsDelete == false)
            {
                jobobj.IsDelete = true;
                db.SaveChanges();
                TempData["Success"] = "Job deleted successfully.";
            }
            else
            {
                TempData["Error"] = "Job not found.";
            }

            return RedirectToAction("index");
        }
        private int GetAdminUserID()
        {
            Actor Admin = (Actor)Session["AuthAdminUser"];
            int adminUserID = 0;
            if (Admin != null)
            {
                adminUserID = Admin.Id;
            }

            return adminUserID;
        }
    }
}