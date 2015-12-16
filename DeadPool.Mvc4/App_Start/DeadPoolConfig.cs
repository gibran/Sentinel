using System;
using DeadPool.Infrastructure;
using DeadPool.Infrastructure.Interfaces;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(DeadPool.Mvc4.App_Start.DeadPoolConfig), "PreStart")]

namespace DeadPool.Mvc4.App_Start
{
    public static class DeadPoolConfig
    {
        public static void PreStart()
        {
            Register(DeadPoolService.Service);
        }

        public static void Register(DeadPoolService service)
        {
            service.Add(new DatabaseTest());
        }
    }

    public class DatabaseTest : ITest
    {
        public string Description
        {
            get { return "Teste de Banco";  }
        }

        public string Name
        {
            get { return "DatabaseTest"; }
        }

        public void Test(ITestContext context)
        {
            context.RaiseEvent(EventType.Success, "Funcionou");
        }
    }
}