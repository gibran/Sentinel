using Sentinel.Core.Interfaces;
using System;
using System.Diagnostics;

namespace Sentinel.Core
{
    public abstract class SentinelTestBase : ITest
    {
        private readonly object _lock = new object();
        private readonly string _name;
        private readonly string _description;
        private readonly Stopwatch _stopwatch = new Stopwatch();

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

                _stopwatch.Restart();

                try
                {
                    _testResult = RunTest();
                }
                catch (Exception e)
                {
                    _testResult = TestResult.CreateFailed(_name, e.Message, _description);
                }

                _stopwatch.Stop();

                if (_testResult != null)
                    _testResult.ElapsedMilliseconds = _stopwatch.ElapsedMilliseconds;

                OnResultChanged(null);
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

        protected abstract TestResult RunTest();

        public event EventHandler ResultChanged;

        private void OnResultChanged(EventArgs e)
        {
            var handler = ResultChanged;
            handler?.Invoke(this, e);
        }
    }
}