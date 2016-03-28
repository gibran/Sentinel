using Microsoft.Owin;
using Owin;
using Sentinel.Core;
using Sentinel.Core.Stores;
using Sentinel.Middleware;
using Sentinel.Middleware.Extensions;
using Sentinel.Mvc4.Sample;
using Sentinel.Tests.Database;
using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;

[assembly: OwinStartup(typeof(Startup))]

namespace Sentinel.Mvc4.Sample
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
                    new DatabaseTest("Database Test", "Check database connection", "DefaultConnection", interval:TimeSpan.FromSeconds(30))
                },
                OnTestResultChange = result =>
                {
                    Console.WriteLine(result.Name);
                }
            });
        }
    }
}