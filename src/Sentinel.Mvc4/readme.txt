﻿﻿Before you can run sentinel, you need to setup Sentinel.

Add the following code in your SentinelConfig.cs:
[assembly: OwinStartup(typeof(Startup))]
namespace <NAMESPACE>
{
	public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseSentinel(new SentinelOptions
            {
                TestResultStore = new InMemoryTestResultStore(),
                Tests = new List<SentinelTestBase>
                {
                    //Add tests class
                },
                OnTestResultChange = result =>
                {
					//Callback code to test runned
                }
            });
        }
    }
}

To configure route to see dashboard, please setup:

            routes.MapRoute(
                name: "DefaultSentinel",
                url: "{controller}/{action}/{id}",
                defaults: new {controller = "Sentinel", action = "Index", id = UrlParameter.Optional},
                 namespaces: new[] { "Sentinel.Mvc4.Controllers" }
                );

To configure tests for your website's sentinel, please see:

	App_Start/Startup.cs