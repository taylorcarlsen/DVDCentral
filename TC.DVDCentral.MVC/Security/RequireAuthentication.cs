using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TC.DVDCentral.MVC.Security
{
    public class RequireAuthentication : FilterAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            if (!Security.Identity.IsAuthenticated())
            {
                filterContext.Result = new RedirectResult("/Account/Login?returnUrl=" + HttpContext.Current.Request.Url);
            }
        }
    }
}