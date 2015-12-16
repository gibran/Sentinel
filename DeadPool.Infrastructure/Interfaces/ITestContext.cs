using DeadPool.Infrastructure.Enums;

namespace DeadPool.Infrastructure.Interfaces
{
    public interface ITestContext
    {
        void RaiseEvent(EventType type, string message);
    }
}