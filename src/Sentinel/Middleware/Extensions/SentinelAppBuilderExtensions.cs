using Autofac.Integration.WebApi;
using Owin;
using System;

namespace Sentinel.Middleware.Extensions
{
    public static class SentinelAppBuilderExtensions
    {
        public static IAppBuilder UseSentinel(this IAppBuilder builder, SentinelOptions options)
        {
            if (builder == null) throw new ArgumentNullException(nameof(builder));
            if (options == null) throw new ArgumentNullException(nameof(options));

            if (!options.Validate())
                return builder;

            var container = AutofacConfiguration.Configure(options);
            var resolver = new AutofacWebApiDependencyResolver(container);
            builder.UseWebApi(WebApiConfig.Configure(resolver));

            return builder;
        }
    }
}