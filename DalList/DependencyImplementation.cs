namespace Dal;
using DalApi;
using DO;
using System.Collections.Generic;
/// <summary>
/// Dependency implementation
/// </summary>
internal class DependencyImplementation : IDependency
{
    public int Create(Dependency item)
    {
        int id = DataSource.Config.NextDependencyId;
        DO.Dependency dependency = item with { Id = id };
        DataSource.Dependencies.Add(dependency);
        return id;
    }

    public void Delete(int id)
    {
        Dependency? deleteDep = DataSource.Dependencies.Find(delDep => delDep.Id == id);
        if (deleteDep != null)
        {
            DataSource.Dependencies.Remove(deleteDep);
        }
        else throw new Exception($"Dependency with ID={id} does Not exist");
    }

    public Dependency? Read(int id)
    {
        return DataSource.Dependencies.Find(item => item.Id == id);
    }

    public List<Dependency> ReadAll()
    {
        return (DataSource.Dependencies);
    }

    public void Update(Dependency item)
    {
        Dependency? updateDep = DataSource.Dependencies.Find(upDep => upDep.Id == item.Id);
        if (updateDep != null)
        {
            DataSource.Dependencies.Remove(updateDep);
            DataSource.Dependencies.Add(item);
        }
        else { throw new Exception($"Dependency with ID={item.Id} does Not exist"); }
    }
}
