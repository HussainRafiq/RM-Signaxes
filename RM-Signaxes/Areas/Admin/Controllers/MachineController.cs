using RM_Signaxes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RM_Signaxes.Areas.Admin.Controllers
{
    [AdminSessionAuthorize]
    public class MachineController : Controller
    {
        RM_Model db = new RM_Model();
        // GET: Admin/Job
        public ActionResult Index()
        {
            var machines = db.Machines.Where(x => x.IsDelete == false);
            return View(machines);
        }


        public ActionResult Create(int? ID = null)
        {
            if (ID != null)
            {
                var machines = db.Machines.Where(x => x.Id == ID && x.IsDelete == false);
                if (machines.Any())
                {
                    return View(machines.ToList()[0]);
                }
                else
                {
                    TempData["Error"] = "Machine not found.";
                }
            }
            else
            {
                return View();
            }
            return RedirectToAction("index");
        }
        [HttpPost]
        public ActionResult Create(Machine machine)
        {

            if (machine.Id == null || machine.Id == 0)
            {

                int adminUserID = GetAdminUserID();
                machine.AddedByID = adminUserID;
                machine.IsDelete = false;
            }
            try
            {
                if (ModelState.IsValid)
                {
                    if (machine.Id == null || machine.Id == 0)
                    {
                        db.Machines.Add(machine);
                        db.SaveChanges();
                        TempData["Success"] = "Machine created successfully";
                    }
                    else
                    {
                        var machineobj = db.Machines.Find(machine.Id);
                        if (machineobj != null && machineobj.IsDelete == false)
                        {
                            machineobj.Make = machine.Make;
                            machineobj.Department = machine.Department;
                            machineobj.Model = machine.Model;
                            machineobj.IsActive = machine.IsActive;
                            machineobj.Area = machine.Area;
                            machineobj.Name = machine.Name;

                            db.SaveChanges();
                            TempData["Success"] = "Machine updated successfully";
                        }
                    }

                    return RedirectToAction("index");

                }
                else
                {
                    TempData["Error"] = "Fill all fields";
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
            var machinobj = db.Machines.Find(ID);
            if (machinobj != null && machinobj.IsDelete == false)
            {
                machinobj.IsDelete = true;
                db.SaveChanges();
                TempData["Success"] = "Machine deleted successfully.";
            }
            else
            {
                TempData["Error"] = "Machine not found.";
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