using Sentinel.Enums;

namespace Sentinel
{
    public class TestResult
    {
        public string Name { get; set; }
        public JobState? JobState { get; set; }
        public EventType? EventType { get; set; }
        public string ErrorMessage { get; set; }
        public string Description { get; set; }

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
                ErrorMessage = errorMessage
            };
        }

        public static TestResult CreateSuccess(string name, string description = null)
        {
            return new TestResult
            {
                Name = name,
                ErrorMessage = null,
                EventType = Enums.EventType.Success,
                Description = description,
                JobState = Enums.JobState.Done
            };
        }
    }
}