using DeadPool.Infrastructure.Interfaces;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace DeadPool.Infrastructure
{
    public class DeadPoolService
    {
        public static DeadPoolService Service = new DeadPoolService();
        private readonly ConcurrentDictionary<string, TestEvaluator> evaluators = new ConcurrentDictionary<string, TestEvaluator>();
        private readonly ConcurrentDictionary<string, ITest> tests = new ConcurrentDictionary<string, ITest>();
        public event EventHandler TestCompleted;

        public event EventHandler TestStarted;

        #region [ Events ]

        protected void OnTestCompleted(object sender, EventArgs e)
        {
            if (TestCompleted != null) TestCompleted(sender, e);
        }

        protected void OnTestStarted(object sender, EventArgs e)
        {
            if (TestStarted != null) TestStarted(sender, e);
        }
        #endregion

        public TestEvaluator Add(ITest test)
        {
            var key = test.Name;

            if (tests.ContainsKey(key))
                throw new Exception($"Already exist test ( {key} ) in tests to verify.");

            if (!tests.TryAdd(key, test))
                throw new Exception($"Was impossible control insert test ( {key} ) in tests to verify.");

            var evaluator = evaluators.GetOrAdd(key, (k) => {
                var e = new TestEvaluator(k, test);
                e.Started += OnTestStarted;
                e.Completed += OnTestCompleted;
                return e;
            });

            return evaluator;
        }

        public IEnumerable<TestEvaluator> GetAll()
        {
            return evaluators.Values.OrderBy(e => e.Name);
        }

        public TestEvaluator GetByKey(string key)
        {
            TestEvaluator evaluator;

            if (evaluators.TryGetValue(key, out evaluator))
                return evaluator;
            else
                throw new Exception(string.Format("Test with this key ( {0} ) cannot be found.", key));
        }
    }
}