using FluentScheduler;
using Sentinel.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sentinel.Core
{
    internal class SentinelService : Registry, ISentinelService
    {
        private readonly List<SentinelTestBase> _tests = new List<SentinelTestBase>();
        private readonly SentinelInitializer _sentinelInitializer;
        private readonly ITestResultStore _testResultStore;

        public SentinelService(ITestResultStore testResultStore)
        {
            _testResultStore = testResultStore;
            _sentinelInitializer = new SentinelInitializer(this);
        }

        public void AddTest(SentinelTestBase test)
        {
            if (_tests.Contains(test))
                throw new Exception("Test already exists.");

            test.ResultChanged += TestOnResultChanged;

            _tests.Add(test);
        }

        private void TestOnResultChanged(object sender, EventArgs eventArgs)
        {
            var test = (SentinelTestBase)sender;
            var result = test.GetResult();

            _testResultStore.Write(test.GetName(), result);
            OnAnyTestResultChanged(result);
        }

        public SentinelInitializer Prepare()
        {
            _tests
                .Select((value, index) => new { value, index })
                .ToList()
                .ForEach(t => Schedule(t.value.Execute).ToRunNow().AndEvery(t.index + 1).Minutes());

            return _sentinelInitializer;
        }

        public TestResult GetResultByTestName(string testName)
        {
            return _testResultStore.Get(testName);
        }

        public IEnumerable<TestResult> GetAllResults()
        {
            return _testResultStore.GetAll();
        }

        public event Action<TestResult> Notifier;

        private void OnAnyTestResultChanged(TestResult testResult)
        {
            Notifier?.Invoke(testResult);
        }
    }
}