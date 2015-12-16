using DeadPool.Infrastructure.Enums;
using DeadPool.Infrastructure.Interfaces;
using DeadPool.Infrastructure.Tests;
using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace DeadPool.Infrastructure
{
    public class TestEvaluator : ITestContext
    {
        private ITest InnerTest { get; set; }
        private Task<TestResult> task;
        private CancellationToken cancelToken;

        public string Key { get; private set; }
        public string Name { get { return InnerTest.Name; } }
        public string Description { get { return InnerTest.Description; } }
        public TestResult Result { get; private set; }

        public TestEvaluator(string key, ITest test)
        {
            this.Key = key;
            this.InnerTest = test;
            this.Result = null;
            this.cancelToken = new CancellationToken();
        }

        private TestResult RunInternal()
        {
            this.Result = null;
            this.OnStarted();

            var result = new TestResult();

            try
            {
                InnerTest.Test(this);
                result.Message = string.Empty;
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
                result.Success = false;
            }

            return result;
        }

        private TestResult RunComplete(Task<TestResult> test)
        {
            this.Result = test.Result;
            this.task = null;
            this.OnCompleted();
            return test.Result;
        }

        public TestResult Run()
        {
            var spentTime = Stopwatch.StartNew();
            var r = this.RunAsync();
            spentTime.Stop();

            var result = r.Result;
            result.SpentTime = spentTime.ElapsedMilliseconds;

            return result;
        }

        private Task<TestResult> RunAsync()
        {
            lock (this)
            {
                if (task == null)
                    task = Task.Run((Func<TestResult>)this.RunInternal, this.cancelToken).ContinueWith(this.RunComplete);

                return task;
            }
        }

        #region [ Events ]

        public event EventHandler Started;
        public event EventHandler Completed;

        protected void OnStarted()
        {
            if (Started != null) Started(this, EventArgs.Empty);
        }

        protected void OnCompleted()
        {
            if (Completed != null) Completed(this, EventArgs.Empty);
        }

        public void RaiseEvent(EventType type, string message)
        {
        }

        #endregion [ Events ]
    }
}