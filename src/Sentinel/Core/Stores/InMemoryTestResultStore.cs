using Sentinel.Core.Interfaces;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Sentinel.Core.Stores
{
    public class InMemoryTestResultStore : ITestResultStore
    {
        private readonly ConcurrentDictionary<string, TestResult> _store = new ConcurrentDictionary<string, TestResult>();

        public void Write(string key, TestResult result)
        {
            _store[key] = result;
        }

        public TestResult Get(string key)
        {
            return _store.ContainsKey(key) ? _store[key] : null;
        }

        public IEnumerable<TestResult> GetAll()
        {
            return _store.Values;
        }
    }
}