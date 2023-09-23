using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace RM_Signaxes
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            
            using (var db = new RM_Model())
            {
                db.Database.Initialize(true);
                var AdminUser = db.Actors.Where(x => x.Role == "Admin" && x.Username == "Admin" && x.IsDelete==false);
                if(AdminUser==null || AdminUser.Count() == 0)
                {
                    db.Actors.Add(new Models.Actor() { 
                        
                    Name="Admin",
                    Username="Admin",
                    Password="1234",
                    AddedByID=0,
                    
                    IsDelete=false,
                    IsActive=true,
                    Role="Admin"                    

                    });
                    db.SaveChanges();
                }
            }
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
