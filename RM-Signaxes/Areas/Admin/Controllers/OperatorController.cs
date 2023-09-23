using RM_Signaxes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RM_Signaxes.Areas.Admin.Controllers
{
    [AdminSessionAuthorize]
    public class OperatorController : Controller
    {

        RM_Model db = new RM_Model();
        // GET: Admin/Operator
        public ActionResult Index()
        {
            var operators = db.Actors.Where(x => x.Role.ToLower() == "operator" && x.IsDelete==false);
            return View(operators);
        }


        public ActionResult Create(int? ID = null)
        {
            if (ID != null)
            {
                var operators = db.Actors.Where(x => x.Id == ID && x.Role.ToLower() == "operator" && x.IsDelete == false);
                if (operators.Any())
                {
                    return View(operators.ToList()[0]);
                }
                else
                {
                    TempData["Error"] = "Operators not found.";
                }
            }
            else
            {
                return View();
            }
            return RedirectToAction("index");            
        }
        [HttpPost]
        public ActionResult Create(Actor actor)
        {
            actor.Role = "Operator";

            if (actor.Id == null || actor.Id == 0)
            {
                var usercount = db.Actors.Where(x => x.Username.ToLower()==actor.Username.ToLower() && x.Role.ToLower()=="operator" && x.IsDelete == false).Count();

                if (usercount > 0)
                {
                    TempData["Error"] = "Username is already taken";
                    return View(actor);
                }

                int adminUserID = GetAdminUserID();
                actor.AddedByID = adminUserID;
                actor.IsDelete = false;
            }
            try
            {
                if (ModelState.IsValid)
                {
                    if (actor.Id == null || actor.Id == 0)
                    {
                        db.Actors.Add(actor);
                        db.SaveChanges();
                        TempData["Success"] = "Operator created successfully";
                    }
                    else
                    {
                        var operatorperson = db.Actors.Find(actor.Id);
                        if (operatorperson!=null && operatorperson.IsDelete==false && operatorperson.Role.ToLower()=="operator")
                        {
                            operatorperson.Name = actor.Name;
                            operatorperson.SkillLevel = actor.SkillLevel;
                            operatorperson.Department = actor.Department;
                            operatorperson.IsActive = actor.IsActive;
                            operatorperson.Designation = actor.Designation;
                            operatorperson.Password = actor.Password;

                            db.SaveChanges();
                            TempData["Success"] = "Operator updated successfully";
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
                var operatorperson = db.Actors.Find(ID);
                if (operatorperson != null && operatorperson.IsDelete == false && operatorperson.Role.ToLower() == "operator")
                {
                   operatorperson.IsDelete=true;
                   db.SaveChanges();
                   TempData["Success"] = "Operator deleted successfully.";
                }
                else
                {
                    TempData["Error"] = "Operators not found.";
                }
            
            return RedirectToAction("index");
        }
        private int GetAdminUserID()
        {
            Actor Admin = (Actor)Session["AuthAdminUser"];
            int adminUserID=0;
            if (Admin != null)
            {
                adminUserID = Admin.Id;
            }

            return adminUserID;
        }
       
    }
}