using DeadPool.Infrastructure.Interfaces;
using System;
using System.Collections.Concurrent;

namespace DeadPool.Infrastructure
{
    public class DeadPoolService
    {
        public static DeadPoolService service = new DeadPoolService();

        private readonly ConcurrentDictionary<string, ITest> tests = new ConcurrentDictionary<string, ITest>();


        public void Add(ITest test)
        {
            var key = test.Name;

            if (tests.ContainsKey(key))
                throw new Exception($"Already exist test ( {key} ) in tests to verify.");

            if (!tests.TryAdd(key, test))
                throw new Exception($"Was impossible control insert test ( {key} ) in tests to verify.");
        }
    }
}