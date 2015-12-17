using System;

namespace DeadPool.Infrastructure
{
    [Serializable]
    public class TestResult
    {
        public string Message { get; internal set; }
        public bool Success { get; internal set; }
        public long SpentTime { get; internal set; }

    }
}