using System.Threading.Tasks;

namespace DeadPool.Infrastructure.Interfaces
{
    public interface ITest
    {
        string Name { get; }
        string Description { get; }

        Task<bool> Test(ITestContext context);
    }
}