using RM_Signaxes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RM_Signaxes.Controllers
{
    public class OperatorController : Controller
    {
        RM_Model RM_Model = new RM_Model();
        // GET: Operator
        [Route("/Operator/Home")]
        public ActionResult Index()
        {
            if (Session["Operator_User"] == null)
            {
                return RedirectToAction("Login");
            }
            Actor actor = (Actor)Session["Operator_User"];

            var Jobs = RM_Model.OperatorsJobs.Include("Machine").Include("Operator").Where(x => x.IsDelete == false && x.OperatorID == actor.Id).ToList();

            if (Jobs != null && Jobs.Count > 0)
            {
               Jobs= Jobs.OrderBy(x => x.Closed == null && x.Status==JobStatus.Open).Reverse().ToList();
            }


            return View(Jobs);
        }


        public ActionResult CreateJob(int? id = null)
        {
            if (Session["Operator_User"] == null)
            {
                return RedirectToAction("Login");
            }

            ViewBag.Machines = RM_Model.Machines.ToList();
            ViewBag.Jobs = RM_Model.Jobs.ToList();
            if (id != null && id != 0)
            {
                var job = RM_Model.OperatorsJobs.Where(x => x.Id == id && x.IsDelete == false);
                if (job.Any())
                {
                    return View(job.First());
                }
                else
                {
                    return HttpNotFound();
                }
            }


            return View();
        }
        [HttpPost]
        public ActionResult CreateJob(OperatorsJobs operatorsJob)
        {
            if (Session["Operator_User"] == null)
            {
                return RedirectToAction("Login");
            }
           
            if (operatorsJob == null)
            {
                TempData["Error"] = "Some Error Occcured";
            }
            else if(operatorsJob.Id==0)
            {
                Actor actor = (Actor)Session["Operator_User"];
                operatorsJob.Closed = null;
                operatorsJob.IsDelete = false;
                operatorsJob.Status = JobStatus.Open;
                operatorsJob.OperatorID = actor.Id;
                RM_Model.OperatorsJobs.Add(operatorsJob);
                RM_Model.SaveChanges();

            }
            else
            {
                var job = RM_Model.OperatorsJobs.Where(x => x.Id == operatorsJob.Id && x.IsDelete == false);
                if (job.Any())
                {
                    var Jobobj = job.First();
                    Jobobj.Description = IsNULL(operatorsJob.Description,Jobobj.Description);
                    Jobobj.Status = IsNULL(operatorsJob.Status,Jobobj.Status);
                    Jobobj.Closed = IsNULL(operatorsJob.Closed, Jobobj.Closed);
                    Jobobj.TotalSpentSeconds = IsNULL(operatorsJob.TotalSpentSeconds*60*60, Jobobj.TotalSpentSeconds);
                    Jobobj.MachineID = IsNULL(operatorsJob.MachineID, Jobobj.MachineID);
                    Jobobj.JobID = IsNULL(operatorsJob.JobID, Jobobj.JobID);

                    RM_Model.SaveChanges();
                }
                else
                {
                    TempData["Error"] = "Job not available";
                }


            }

            return RedirectToAction("Index");
        }

        private T IsNULL<T>(T Value1,T Value2)
        {
            return Value1==null ? Value2 : Value1;
        }

        [Route("/Operator/Login")]
        public ActionResult Login()
        {
            if (Session["Operator_User"] != null)
            {
                RedirectToAction("Index");
            }

            return View();
        }
        [Route("/Operator/Login")]
        [HttpPost]
        public ActionResult Login(FormCollection form)
        {
            if (form != null && form.AllKeys.Contains("Username") && form.AllKeys.Contains("Password") && !String.IsNullOrEmpty(form["Username"]) && !String.IsNullOrEmpty(form["Password"]))
            {
                String Username = form["Username"].ToLower();
                String Password = form["Password"].ToLower();
                var user = RM_Model.Actors.Where(x => x.Username.ToLower() == Username && x.Password == Password && x.IsDelete == false && x.Role.ToLower() == "operator");
                if (user != null && user.Any())
                {
                    Session["Operator_User"] = user.First();
                    return RedirectToAction("Index");
                }
            }
            if (form.AllKeys.Contains("Username"))
                ViewBag.Username = form["Username"];

            ViewBag.Error = "Incorrect username or password";

            return View();
        }
        public ActionResult Logout()
        {
            if (Session["Operator_User"] == null)
            {
                return RedirectToAction("Login");
            }
            Session.RemoveAll();
            Session.Clear();


            return RedirectToAction("Login");
        }
    }
}