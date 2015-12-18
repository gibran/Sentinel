using System.Collections.Generic;

namespace Sentinel.Interfaces
{
    public interface ISentinelService
    {
        void Add(SentinelTestBase test);

        void Prepare();

        TestResult GetResultByTestName(string testName);

        IEnumerable<TestResult> GetAllResults();
    }
}