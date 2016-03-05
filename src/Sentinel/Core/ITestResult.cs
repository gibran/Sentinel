using Sentinel.Core.Enums;

namespace Sentinel.Core
{
    public interface ITestResult
    {
        string Name { get; set; }
        JobState? JobState { get; set; }
        EventType? EventType { get; set; }
        string Message { get; set; }
        string Description { get; set; }
    }
}