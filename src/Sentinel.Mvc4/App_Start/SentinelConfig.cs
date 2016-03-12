using Microsoft.Owin;
using Owin;
using Sentinel.Core;
using Sentinel.Core.Stores;
using Sentinel.Middleware;
using Sentinel.Middleware.Extensions;
using Sentinel.Mvc4.App_Start;
using Sentinel.Tests.Database;
using System;
using System.Collections.Generic;

[assembly: OwinStartup(typeof(SentinelConfig))]

namespace Sentinel.Mvc4.App_Start
{
    public class SentinelConfig
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseSentinel(new SentinelOptions
            {
                TestResultStore = new InMemoryTestResultStore(),
                Tests = new List<SentinelTestBase>
                {
                    new DatabaseTest("Teste Conexão", "Não possui descrição", "DefaultConnection")
                },
                OnTestResultChange = result =>
                {
                    Console.WriteLine(result.Name);
                }
            });
        }
    }
}