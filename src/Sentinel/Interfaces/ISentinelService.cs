using Sentinel.Result;
using System.Collections.Generic;

namespace Sentinel.Interfaces
{
    public interface ISentinelService
    {
        void Add(SentinelTestBase test);

        SentinelInitializer Prepare();

        TestResult GetResultByTestName(string testName);

        IEnumerable<TestResult> GetAllResults();
    }
}