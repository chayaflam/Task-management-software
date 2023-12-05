
using DalApi;
using System.Data.SqlTypes;

namespace Dal;

sealed internal class DalList : IDal
{
    public static IDal Instance { get; } = new Lazy<DalList>(() => new DalList(),true).Value;
    private DalList() { }

    public IEngineer Engineer => new EngineerImplementation();

    public ITask Task =>  new TaskImplementation();

    public IDependency Dependency =>  new DependencyImplementation();
}
