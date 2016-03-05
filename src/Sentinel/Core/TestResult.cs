using Newtonsoft.Json;
using Sentinel.Core.Enums;

namespace Sentinel.Core
{
    public class TestResult : ITestResult
    {
        public string Name { get; set; }

        [JsonIgnore]
        public JobState? JobState { get; set; }

        [JsonIgnore]
        public EventType? EventType { get; set; }

        public string Message { get; set; }
        public string Description { get; set; }
        public long ElapsedMilliseconds { get; set; }

        public string JobStateName => JobState?.ToString();
        public string EventTypeName => EventType?.ToString();

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