using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RM_Signaxes.Controllers
{
    public class HomeController : Controller
    {
        RM_Model RM_Model = new RM_Model();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetJobs()
        {

            var currdate = DateTime.Now.Date;
            RM_Model.Configuration.ProxyCreationEnabled = false;
            var operatorjobs = RM_Model.OperatorsJobs.Include("Machine").Include("Job").Include("Operator").Where(x =>( x.Status.ToString().ToLower() == "open" || (EntityFunctions.TruncateTime(x.Opened) >= currdate && (x.Closed.HasValue && EntityFunctions.TruncateTime(x.Closed.Value) <= currdate))) && (!x.IsDelete.HasValue || !x.IsDelete.Value)).ToList();
            var list = JsonConvert.SerializeObject(operatorjobs, Formatting.None, new JsonSerializerSettings()
            {
                ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            });

            return Content(list, "application/json");
        }


    }
}