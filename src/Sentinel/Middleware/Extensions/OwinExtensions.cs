using Autofac;
using Microsoft.Owin;
using System.Collections.Generic;

namespace Sentinel.Middleware.Extensions
{
    static class OwinExtensions
    {
        internal static ILifetimeScope GetLifetimeScope(this IDictionary<string, object> env)
        {
            return new OwinContext(env).Get<ILifetimeScope>("sentinel:autofacscope");
        }

        internal static void SetLifetimeScope(this IDictionary<string, object> env, ILifetimeScope scope)
        {
            new OwinContext(env).Set<ILifetimeScope>("sentinel:autofacscope", scope);
        }
    }
}