namespace DeadPool.Infrastructure.Interfaces
{
    public interface ITest
    {
        string Name { get; }
        string Description { get; }
        void Test(ITestContext context);
    }
}