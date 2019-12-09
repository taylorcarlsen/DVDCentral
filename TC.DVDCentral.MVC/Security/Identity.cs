using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TC.DVDCentral.Models;

namespace TC.DVDCentral.MVC.Security
{
    public class Identity
    {
        public static bool IsAuthenticated()
        {
            return HttpContext.Current.Session["USER"] != null;
        }

        public static void Authenticate(User user)
        {
            HttpContext.Current.Session["User"] = user;
        }

        public static void RemoveCurrentUserAuthentication()
        {
            HttpContext.Current.Session["USER"] = null;
        }

        public static User GetCurrentUser()
        {
            if (HttpContext.Current.Session["USER"] == null)
                return null;
            else
                return (User)HttpContext.Current.Session["USER"];
        }
    }
}