namespace DeadPool.Infrastructure.Interfaces
{
    public interface ITest
    {
        string Name { get; set; }
        string Description { get; set; }
        void Test();
    }
}