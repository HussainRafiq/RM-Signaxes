using RM_Signaxes.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RM_Signaxes.Areas.Admin.Controllers
{
    public class AuthController : Controller
    {
        RM_Model db = new RM_Model();
        // GET: Admin/Auth
        public ActionResult Login()
        {
            if(Session["AuthAdminUser"]!=null)
                return RedirectToAction("index", "Home");

            return View();
        }
        [HttpPost]
        public ActionResult Login(LoginAuth loginAuth)
        {



            if (loginAuth != null)
            {
                var AdminUser = db.Actors.Where(x => x.Role == "Admin" && x.Username == loginAuth.Username && x.IsDelete == false);
                if(AdminUser!=null && AdminUser.Any())
                {

                    if (AdminUser.ToList()[0].Password==loginAuth.Password)
                    {
                        if (AdminUser.ToList()[0].IsActive.HasValue && AdminUser.ToList()[0].IsActive.Value) {

                            Session["AuthAdminUser"] = AdminUser.ToList()[0];
                            return RedirectToAction("index", "Home");
                        }
                        else
                        {
                            ViewBag.Error = "User is inactive";
                            return View();
                        }
                    }
                   
                }

                ViewBag.Error = "Invalid Username or Password";
                return View();
            }
            return View();
        }
        public ActionResult Logout()
        {
            Session.Clear();

            return RedirectToAction("index", "Home");
        }
    }
}