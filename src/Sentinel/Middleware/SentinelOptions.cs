using Sentinel.Core;
using Sentinel.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sentinel.Middleware
{
    public class SentinelOptions
    {
        public ITestResultStore TestResultStore { get; set; }
        public IEnumerable<SentinelTestBase> Tests { get; set; }
        public Action<TestResult> OnTestResultChange { internal get; set; }

        internal bool Validate()
        {
            if (TestResultStore == null)
                throw new Exception("Test Result Store must be specified.");

            return Tests != null && Tests.Any();
        }
    }
}