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

        public void Validate()
        {
            if (TestResultStore == null)
                throw new Exception("Test Result Store must be specified.");

            if (Tests == null)
                throw new Exception("No tests found.");

            if (!Tests.Any())
                throw new Exception("No tests found.");
        }
    }
}