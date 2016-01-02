using Sentinel.Core.Interfaces;
using System;

namespace Sentinel.Core
{
    public abstract class SentinelTestBase : ITest
    {
        private readonly object _lock = new object();
        private readonly string _name;
        private readonly string _description;

        private bool _shuttingDown;
        private TestResult _testResult;

        protected SentinelTestBase(string name, string description)
        {
            _name = name;
            _description = description;
            _testResult = TestResult.CreateNew(GetName());
        }

        public void Shutdown()
        {
            _shuttingDown = true;
        }

        public void Execute()
        {
            lock (_lock)
            {
                if (_shuttingDown)
                    return;

                RunTest();
            }
        }

        public string GetName()
        {
            return _name;
        }

        public string GetDescription()
        {
            return _description;
        }

        public TestResult GetResult()
        {
            return _testResult;
        }

        protected void SetResult(TestResult testResult)
        {
            _testResult = testResult;
            OnResultChanged(null);
        }

        protected abstract void RunTest();

        public event EventHandler ResultChanged;

        private void OnResultChanged(EventArgs e)
        {
            var handler = ResultChanged;
            handler?.Invoke(this, e);
        }
    }
}