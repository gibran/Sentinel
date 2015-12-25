using FluentScheduler;
using Sentinel.Result;

namespace Sentinel.Interfaces
{
    internal interface ITest : ITask
    {
        string GetName();

        string GetDescription();

        TestResult GetResult();

        void Shutdown();
    }
}