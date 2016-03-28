using FluentScheduler;

namespace Sentinel.Core
{
    internal interface ITest : IJob
    {
        string GetName();

        string GetDescription();

        TestResult GetResult();

        void Shutdown();
    }
}