using System.Collections.Generic;

namespace Sentinel.Core
{
    public interface ITestResultStore
    {
        void Write(string key, TestResult result);

        TestResult Get(string key);

        IEnumerable<TestResult> GetAll();
    }
}