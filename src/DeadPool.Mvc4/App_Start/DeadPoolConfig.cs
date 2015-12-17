using DeadPool.Infrastructure;
using DeadPool.Infrastructure.Tests;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(DeadPool.Mvc4.App_Start.DeadPoolConfig), "PreStart")]

namespace DeadPool.Mvc4.App_Start
{
    public static class DeadPoolConfig
    {
        public static void PreStart()
        {
            Register(DeadPoolService.Instance);
        }

        public static void Register(DeadPoolService service)
        {
            service.Add(new DatabaseTest("LocalSqlServer"));
        }
    }
}