﻿using System;
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
        private readonly TimeSpan _interval;
        private TestResult _testResult;

        protected SentinelTestBase(string name, string description, TimeSpan interval)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name));

            if (string.IsNullOrWhiteSpace(description))
                throw new ArgumentNullException(nameof(description));

            if (interval < TimeSpan.FromSeconds(1))
                throw new Exception("Sorry, but the test interval should not be less than 1 second.");

            _name = name;
            _description = description;
            _interval = interval;
            _testResult = TestResult.CreateNew(GetName(), GetDescription());
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
                    _testResult = TestResult.CreateFailed(_name, _description, e.Message);
                }

                _stopwatch.Stop();

                if (_testResult != null)
                    _testResult.ElapsedMilliseconds = _stopwatch.ElapsedMilliseconds;

                OnResultChanged(null);
            }
        }

        public TimeSpan GetInterval()
        {
            return _interval;
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