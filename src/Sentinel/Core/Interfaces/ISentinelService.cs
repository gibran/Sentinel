using System.Collections.Generic;

namespace Sentinel.Core.Interfaces
{
    public interface ISentinelService
    {
        void AddTest(SentinelTestBase test);

        SentinelInitializer Prepare();

        TestResult GetResultByTestName(string testName);

        IEnumerable<TestResult> GetAllResults();
    }
}