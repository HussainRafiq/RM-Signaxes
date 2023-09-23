using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RM_Signaxes.Areas.Admin.Controllers
{
    [AdminSessionAuthorizeAttribute]
    public class HomeController : Controller
    {

        RM_Model db = new RM_Model();
        // GET: Admin/Home
        public ActionResult Index()
        {
            var operators = db.Actors.Where(x => x.Role.ToLower() == "operator" && x.IsDelete == false);
            var machines = db.Machines.Where(x => x.IsDelete == false);
            var predefinedjobs = db.Jobs.Where(x => x.IsDelete == false);
            var OpenedJobs = db.OperatorsJobs.Where(x => x.IsDelete == false && x.Closed==null && x.Status.ToLower()=="open");

            ViewBag.OperatorsCount = operators.Count();
            ViewBag.JobsCount = predefinedjobs.Count();
            ViewBag.MachinesCount = machines.Count();
            ViewBag.OpenedJobs = OpenedJobs.Count();







            return View();
        }
    }
}