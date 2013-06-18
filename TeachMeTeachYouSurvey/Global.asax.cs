﻿using System;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using TeachMeTeachYouSurvey.Models;

namespace TeachMeTeachYouSurvey
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AuthConfig.RegisterAuth();

            Database.SetInitializer<TeachMeTeachYouDB>(null);
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            var request = HttpContext.Current.Request;
            var logText = new StringBuilder();

            logText.AppendLine("#exception");
            logText.AppendLine(Server.GetLastError().ToString());

            try
            {
                logText
                    .AppendLine("#request")
                    .AppendLine(request.HttpMethod + " " + request.RawUrl)
                    .AppendLine(request.ServerVariables["ALL_RAW"]);
            }
            catch { }

            try
            {
                logText.AppendLine("#form");
                foreach (var key in request.Form.AllKeys)
                {
                    logText.AppendLine(key + "=" + request.Form[key]);
                }
            }
            catch { }

            try
            {
                logText.AppendLine("#server-variables");
                foreach (var key in request.ServerVariables.AllKeys.Where(k => new[] { "ALL_HTTP", "ALL_RAW" }.Contains(k) == false))
                {
                    logText.AppendLine(key + "=" + request.ServerVariables[key]);
                }
            }
            catch { }

            try { Trace.TraceError(logText.ToString()); }
            catch { }
        }
    }
}