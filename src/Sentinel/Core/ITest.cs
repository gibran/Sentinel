using FluentScheduler;
using System;

namespace Sentinel.Core
{
    internal interface ITest : IJob
    {
        TimeSpan GetInterval();

        string GetName();

        string GetDescription();

        TestResult GetResult();

        void Shutdown();
    }
}