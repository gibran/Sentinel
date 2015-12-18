using FluentScheduler;

namespace Sentinel.Interfaces
{
    public interface ITest : ITask
    {
        string GetName();

        string GetDescription();

        TestResult GetResult();

        void SetResult(TestResult testResult);

        void Shutdown();
    }
}