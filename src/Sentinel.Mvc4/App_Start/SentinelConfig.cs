using FluentScheduler;
using Sentinel.Mvc4;
using Sentinel.Stores;
using Sentinel.Tests.Database;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(SentinelConfig), "PreStart")]

namespace Sentinel.Mvc4
{
    public static class SentinelConfig
    {
        public static readonly SentinelService Instance = new SentinelService(new InMemoryTestResultStore());

        public static void PreStart()
        {
            Register(Instance);
        }

        static void Register(SentinelService service)
        {
            service.Add(new DatabaseTest("Databases", "Databases"));
            service.Prepare();
            TaskManager.Initialize(service);
        }
    }
}