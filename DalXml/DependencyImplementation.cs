
using DalApi;
using DO;

namespace Dal;

internal class DependencyImplementation : IDependency
{
    string s_Dependency = "dependencies";
    public int Create(Dependency item)
    {
        List<DO.Dependency> listDep = XMLTools.LoadListFromXMLSerializer<Dependency>(s_Dependency)!;
        int id = Config.NextDependencyId;
        DO.Dependency dependency = item with { Id = id };
        listDep.Add(dependency);
        XMLTools.SaveListToXMLSerializer(listDep, s_Dependency);
        return id;
    }

    public void Delete(int id)
    {

        List<DO.Dependency> listDep = XMLTools.LoadListFromXMLSerializer<Dependency>(s_Dependency)!;
        int find = listDep.RemoveAll(dep => dep.Id == id);
        if (find == 0)
            throw new DalDoesNotExistException($"Dependency with ID={id} does Not exist");
        XMLTools.SaveListToXMLSerializer(listDep, s_Dependency);
    }

    public Dependency? Read(int id)
    {
        List<DO.Dependency> listDep = XMLTools.LoadListFromXMLSerializer<Dependency>(s_Dependency)!;
        Dependency? dep = listDep.FirstOrDefault(item => item.Id == id);
        if (dep == null)
            throw new DalDoesNotExistException($"Dependency with ID={id} does Not exist");
        return dep;
    }

    public Dependency? Read(Func<Dependency, bool> filter)
    {
        List<DO.Dependency> listDep = XMLTools.LoadListFromXMLSerializer<Dependency>(s_Dependency)!;
        Dependency? dep = listDep.FirstOrDefault(filter);
        if (dep == null)
            throw new DalDoesNotExistException("This dependency does Not exist");
        return dep;
    }

    public IEnumerable<Dependency?> ReadAll(Func<Dependency?, bool>? filter = null)
    {
        List<DO.Dependency> listDep = XMLTools.LoadListFromXMLSerializer<Dependency>(s_Dependency)!;
        if (filter == null)
            return listDep.Select(item => item);
        else
            return listDep.Where(filter);
    }

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
