using Microsoft.Owin;
using Owin;
using Sentinel.Core;
using Sentinel.Core.Stores;
using Sentinel.Middleware;
using Sentinel.Middleware.Extensions;
using Sentinel.Mvc4;
using Sentinel.Tests.Database;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;

[assembly: OwinStartup(typeof(Startup))]

namespace Sentinel.Mvc4
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            app.UseSentinel(new SentinelOptions
            {
                TestResultStore = new InMemoryTestResultStore(),
                Tests = new List<SentinelTestBase>
                {
                    new DatabaseTest("test", "test", "DefaultConnection")
                }
            });
        }
    }
}