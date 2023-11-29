

using DalApi;
namespace Dal;

sealed internal class DalXml : IDal
{
static private IDal? instance;
public static IDal Instance { get; } = (new Lazy<DalXml>()).Value;
    //{ get
    //    {
    //        if (instance == null)
    //            lock (instance!)
    //            { instance = new Lazy<DalXml>().Value; }
    //        return instance;
    //    }


    private DalXml() { }
    public IEngineer Engineer => new EngineerImplementation();
    public ITask Task => new TaskImplementation();
    public IDependency Dependency => new DependencyImplementation();
}
