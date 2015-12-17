using DeadPool.Infrastructure.Enums;
using DeadPool.Infrastructure.Interfaces;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace DeadPool.Infrastructure
{
    public class TestEvaluator : ITestContext
    {
        public string Name => InnerTest.Name;
        public string Description => InnerTest.Description;
        ITest InnerTest { get; }
        public TestResult Result { get; private set; }

        public TestEvaluator(ITest test)
        {
            InnerTest = test;
        }

        public async Task Run()
        {
            OnStarted();

            Result = new TestResult();

            try
            {
                var timeSpent = Stopwatch.StartNew();

                var result = await InnerTest.Test(this);

                timeSpent.Stop();

                if (!result)
                    Result.ErrorMessage = "Test explicitly failed.";

                Result.TimeSpent = TimeSpan.FromMilliseconds(timeSpent.ElapsedMilliseconds);
            }
            catch (Exception e)
            {
                Result = new TestResult { ErrorMessage = e.Message };
            }

            OnCompleted();
        }

        #region [Events]

        public event EventHandler Started;

        public event EventHandler Completed;

        public void RaiseEvent(EventType type, string message)
        {
        }

        void OnCompleted()
        {
            Completed?.Invoke(this, EventArgs.Empty);
        }

        void OnStarted()
        {
            Started?.Invoke(this, EventArgs.Empty);
        }

        #endregion [Events]
    }
}