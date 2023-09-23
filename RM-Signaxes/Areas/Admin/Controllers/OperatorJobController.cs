using RM_Signaxes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RM_Signaxes.Areas.Admin.Controllers
{
    [AdminSessionAuthorize]
    public class OperatorJobController : Controller
    {
        RM_Model db = new RM_Model();

        public ActionResult Job(String ID,String Type)
        {
            List<OperatorsJobs> operatorsJobs=null;

            if (Type.ToLower() == "machine")
            {
                var machine = db.Machines.Where(x=>x.Id.ToString()==ID && x.IsDelete==false);
                if(machine.Any())
                {
                    ViewBag.Title = machine.First().Name+" "+ machine.First().Model + " " + machine.First().Make+" Jobs";
                }
                operatorsJobs = db.OperatorsJobs.Where(x=>x.Machine.Id.ToString()==ID && x.IsDelete==false).ToList();
            }
            else if(Type.ToLower() == "job")
            {
                var jobs = db.Jobs.Where(x => x.Id.ToString() == ID && x.IsDelete == false);
                if (jobs.Any())
                {
                    ViewBag.Title = jobs.First().Title+" Jobs";
                }
                operatorsJobs = db.OperatorsJobs.Where(x => x.Job.Id.ToString() == ID && x.IsDelete == false).ToList();
            }
            else if (Type.ToLower() == "operator")
            {
                var operators = db.Actors.Where(x => x.Id.ToString() == ID && x.Role.ToLower()=="operator" && x.IsDelete == false);
                if (operators.Any())
                {
                    ViewBag.Title = operators.First().Name +" | "+operators.First().Department+" Jobs" ;
                }
                operatorsJobs = db.OperatorsJobs.Where(x => x.Operator.Id.ToString() == ID && x.IsDelete == false).ToList();
            }

            return View(operatorsJobs);
        }
    }
}