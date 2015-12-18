using Sentinel.Annotations;
using Sentinel.Interfaces;
using System;
using System.ComponentModel;

namespace Sentinel
{
    public abstract class SentinelTestBase : ITest
    {
        readonly object _lock = new object();
        readonly string _name;
        readonly string _description;

        bool _shuttingDown;
        TestResult _testResult;

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

        public void SetResult(TestResult testResult)
        {
            _testResult = testResult;
            OnResultChanged(null);
        }

        protected abstract void RunTest();

        public event EventHandler ResultChanged;

        void OnResultChanged(EventArgs e)
        {
            var handler = ResultChanged;
            handler?.Invoke(this, e);
        }
    }
}