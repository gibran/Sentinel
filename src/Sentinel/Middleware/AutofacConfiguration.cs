using Autofac;
using Autofac.Integration.WebApi;
using Sentinel.Core;
using Sentinel.Core.Interfaces;
using System;

namespace Sentinel.Middleware
{
    public static class AutofacConfiguration
    {
        public static IContainer Configure(SentinelOptions options)
        {
            if (options == null) throw new ArgumentNullException(nameof(options));

            var builder = new ContainerBuilder();

            var sentinelService = CreateService(options);
            builder.RegisterInstance<ISentinelService>(sentinelService);
            builder.RegisterApiControllers(typeof(AutofacConfiguration).Assembly);

            var container = builder.Build();

            return container;
        }

        static SentinelService CreateService(SentinelOptions options)
        {
            var sentinelService = new SentinelService(options.TestResultStore);
            foreach (var test in options.Tests)
                sentinelService.AddTest(test);

            sentinelService.Prepare().Start();
            return sentinelService;
        }
    }
}