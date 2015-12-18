using FluentScheduler;
using Sentinel.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sentinel
{
    public class SentinelService : Registry, ISentinelService
    {
        readonly List<SentinelTestBase> _tests = new List<SentinelTestBase>();
        readonly ITestResultStore _testResultStore;

        public SentinelService(ITestResultStore testResultStore)
        {
            _testResultStore = testResultStore;
        }

        public void Add(SentinelTestBase test)
        {
            if (_tests.Contains(test))
                throw new Exception("Test already exists.");

            test.ResultChanged += TestOnResultChanged;

            _tests.Add(test);
        }

        void TestOnResultChanged(object sender, EventArgs eventArgs)
        {
            var test = (SentinelTestBase)sender;
            _testResultStore.Write(test.GetName(), test.GetResult());
        }

        public void Prepare()
        {
            _tests
                .Select((value, index) => new { value, index })
                .ToList()
                .ForEach(t => Schedule(t.value.Execute).ToRunNow().AndEvery(t.index + 1).Minutes());
        }

        public TestResult GetResultByTestName(string testName)
        {
            return _testResultStore.Get(testName);
        }

        public IEnumerable<TestResult> GetAllResults()
        {
            return _testResultStore.GetAll();
        }
    }
}