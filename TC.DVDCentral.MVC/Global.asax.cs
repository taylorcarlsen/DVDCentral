using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using TC.DVDCentral.Models;

namespace TC.DVDCentral.MVC
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Application_Error(Object sender, EventArgs e)
        {
            Exception ex = Server.GetLastError();
            LogException(ex);
            Server.ClearError();
        }

        private void LogException(Exception ex)
        {
            ErrorRecord error = new ErrorRecord();
            error.UserIp = Request.UserHostAddress;
            error.Url = Request.Url.ToString();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44354/api/");

                var postTask = client.PostAsJsonAsync<ErrorRecord>("errors", error);
                postTask.Wait();
                var result = postTask.Result;
            }
        }
    }
}
