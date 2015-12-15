using DeadPool.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace DeadPool.Infrastructure
{
    internal class TestMonitor
    {
        private ITest Test { get; set; }
        private Task<TestResult> task;
        private CancellationToken cancelToken;

        public string Key { get; private set; }
        public string Name { get { return Test.Name; } }

        public List<TestEvent> Events { get; private set; }
        public TestResult Result { get; private set; }

        public TestMonitor(string key, ITest test)
        {
            this.Key = key;
            this.Test = test;
            this.Result = null;
            this.Events = new List<TestEvent>();
            this.cancelToken = new CancellationToken();
        }

        private TestResult Run()
        {
            throw new NotImplementedException();
        }

        private TestResult RunComplete(Task<TestResult> task)
        {
            throw new NotImplementedException();
        }

        private Task<TestResult> RunAsync()
        {
            lock (this)
            {
                if (task == null)
                    task = Task.Run<TestResult>((Func<TestResult>)this.Run, this.cancelToken).ContinueWith<TestResult>(this.RunComplete);

                return task;
            }
        }

        #region [ Events ]

        public event EventHandler Started;
        public event EventHandler Completed;
        public event EventHandler<TestEvent> EventReceived;

        protected void OnStarted()
        {
            if (Started != null) Started(this, EventArgs.Empty);
        }
        protected void OnCompleted()
        {
            if (Completed != null) Completed(this, EventArgs.Empty);
        }
        protected void OnTestEvent(TestEvent e)
        {
            this.Events.Add(e);
            if (EventReceived != null) EventReceived(this, e);
        }

        #endregion

    }
}