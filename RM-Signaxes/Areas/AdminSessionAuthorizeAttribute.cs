using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace RM_Signaxes.Areas
{
    public class AdminSessionAuthorizeAttribute : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            return System.Web.HttpContext.Current.Session["AuthAdminUser"] != null;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new RedirectToRouteResult(
                              new RouteValueDictionary
                              {
                                   { "action", "Login" },
                                   { "controller", "Auth" }
                              });
        }
    }

}
