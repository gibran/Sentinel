using FluentScheduler;

namespace Sentinel.Core.Interfaces
{
    internal interface ITest : ITask
    {
        string GetName();

        string GetDescription();

        TestResult GetResult();

        void Shutdown();
    }
}