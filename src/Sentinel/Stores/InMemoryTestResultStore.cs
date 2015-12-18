using Sentinel.Interfaces;
using System.Collections.Generic;

namespace Sentinel.Stores
{
    public class InMemoryTestResultStore : ITestResultStore
    {
        readonly Dictionary<string, TestResult> _store = new Dictionary<string, TestResult>();

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