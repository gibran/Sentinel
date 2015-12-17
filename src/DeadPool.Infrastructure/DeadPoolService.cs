using DeadPool.Infrastructure.Interfaces;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace DeadPool.Infrastructure
{
    public class DeadPoolService
    {
        public static readonly DeadPoolService Instance = new DeadPoolService();
        readonly ConcurrentDictionary<string, TestEvaluator> _evaluators = new ConcurrentDictionary<string, TestEvaluator>();
        readonly ConcurrentDictionary<string, ITest> _tests = new ConcurrentDictionary<string, ITest>();

        public event EventHandler TestCompleted;

        public event EventHandler TestStarted;

        public TestEvaluator Add(ITest test)
        {
            var key = test.Name;

            if (_tests.ContainsKey(key))
                throw new Exception($"Already exist test ( {key} ) in tests to verify.");

            if (!_tests.TryAdd(key, test))
                throw new Exception($"Could not add test ( {key} ) in tests to verify.");

            var evaluator = _evaluators.GetOrAdd(key, (k) =>
            {
                var testEvaluator = new TestEvaluator(test);
                testEvaluator.Started += OnTestStarted;
                testEvaluator.Completed += OnTestCompleted;
                return testEvaluator;
            });

            return evaluator;
        }

        public IEnumerable<TestEvaluator> GetAll()
        {
            return _evaluators.Values.OrderBy(e => e.Name);
        }

        public TestEvaluator GetByKey(string key)
        {
            if (!_evaluators.ContainsKey(key))
                throw new Exception($"Test {key} cannot be found.");

            return _evaluators[key];
        }

        void OnTestCompleted(object sender, EventArgs e)
        {
            TestCompleted?.Invoke(sender, e);
        }

        void OnTestStarted(object sender, EventArgs e)
        {
            TestStarted?.Invoke(sender, e);
        }
    }
}