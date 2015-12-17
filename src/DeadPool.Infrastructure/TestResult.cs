using System;

namespace DeadPool.Infrastructure
{
    public class TestResult
    {
        public string ErrorMessage { get; set; }
        public TimeSpan? TimeSpent { get; set; }
        public bool Success => ErrorMessage == null && TimeSpent != null;
    }
}