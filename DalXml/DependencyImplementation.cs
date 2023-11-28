
using DalApi;
using DO;

namespace Dal;

internal class DependencyImplementation : IDependency
{
    string s_Dependency = "dependencies";

    /// <summary>
    /// create dependency
    /// </summary>
    public int Create(Dependency item)
    {
        List<DO.Dependency> listDep = XMLTools.LoadListFromXMLSerializer<Dependency>(s_Dependency)!;
        int id = Config.NextDependencyId;
        DO.Dependency dependency = item with { Id = id };
        listDep.Add(dependency);
        XMLTools.SaveListToXMLSerializer(listDep, s_Dependency);
        return id;
    }
    /// <summary>
    /// delete dependency
    /// </summary>
    public void Delete(int id)
    {

        List<DO.Dependency> listDep = XMLTools.LoadListFromXMLSerializer<Dependency>(s_Dependency)!;
        int find = listDep.RemoveAll(dep => dep.Id == id);
        if (find == 0)
            throw new DalDoesNotExistException($"Dependency with ID={id} does Not exist");
        XMLTools.SaveListToXMLSerializer(listDep, s_Dependency);
    }
    /// <summary>
    /// read dependency by id
    /// </summary>
    public Dependency? Read(int id)
    {
        List<DO.Dependency> listDep = XMLTools.LoadListFromXMLSerializer<Dependency>(s_Dependency)!;
        Dependency? dep = listDep.FirstOrDefault(item => item.Id == id);
        if (dep == null)
            throw new DalDoesNotExistException($"Dependency with ID={id} does Not exist");
        return dep;
    }
    /// <summary>
    /// read a dependency according to a certain condition
    /// </summary>
    public Dependency? Read(Func<Dependency, bool> filter)
    {
        List<DO.Dependency> listDep = XMLTools.LoadListFromXMLSerializer<Dependency>(s_Dependency)!;
        Dependency? dep = listDep.FirstOrDefault(filter);
        if (dep == null)
            throw new DalDoesNotExistException("This dependency does Not exist");
        return dep;
    }
    /// <summary>
    /// read all dependencies
    /// </summary>
    public IEnumerable<Dependency?> ReadAll(Func<Dependency?, bool>? filter = null)
    {
        List<DO.Dependency> listDep = XMLTools.LoadListFromXMLSerializer<Dependency>(s_Dependency)!;
        if (filter == null)
            return listDep.Select(item => item);
        else
            return listDep.Where(filter);
    }
    /// <summary>
    /// update dependency
    /// </summary>
    public void Update(Dependency item)
    {
        List<DO.Dependency> listDep = XMLTools.LoadListFromXMLSerializer<Dependency>(s_Dependency)!;
        int find = listDep.RemoveAll(dep => dep.Id == item.Id);
        if (find == 0) throw new DalDoesNotExistException($"Dependency with ID={item.Id} does Not exist");
        else
        {
            listDep.Add(item);
            XMLTools.SaveListToXMLSerializer(listDep, s_Dependency);
        }
    }
}
