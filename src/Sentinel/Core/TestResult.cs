using Sentinel.Core.Enums;

namespace Sentinel.Core
{
    public class TestResult
    {
        public string Name { get; set; }
        public JobState? JobState { get; set; }
        public EventType? EventType { get; set; }
        public string Message { get; set; }
        public string Description { get; set; }

        protected TestResult()
        {
        }

        public static TestResult CreateNew(string name)
        {
            return new TestResult { Name = name, JobState = Enums.JobState.New };
        }

        public static TestResult CreateFailed(string name, string errorMessage, string description = null)
        {
            return new TestResult
            {
                Name = name,
                JobState = Enums.JobState.Done,
                EventType = Enums.EventType.Fail,
                Description = description,
                Message = errorMessage
            };
        }

        public static TestResult CreateSuccess(string name, string description = null)
        {
            return new TestResult
            {
                Name = name,
                Message = "Test passed.",
                EventType = Enums.EventType.Success,
                Description = description,
                JobState = Enums.JobState.Done
            };
        }
    }
}