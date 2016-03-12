﻿using Sentinel.Mvc4.App_Start;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;

namespace Sentinel.Mvc4
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
    }
}