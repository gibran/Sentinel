using System.Collections.Generic;

namespace Sentinel.Core
{
    public interface ISentinelService
    {
        void AddTest(SentinelTestBase test);

        TestResult GetResultByTestName(string testName);

        IEnumerable<TestResult> GetAllResults();
    }
}