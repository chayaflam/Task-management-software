namespace Dal;
using DalApi;
using DO;
using System.Collections.Generic;
/// <summary>
/// Dependency implementation
/// </summary>
internal class DependencyImplementation : IDependency
{
    /// <summary>
    /// create dependency
    /// </summary>
    public int Create(Dependency item)
    {
        int id = DataSource.Config.NextDependencyId;
        DO.Dependency dependency = item with { Id = id };
        DataSource.Dependencies.Add(dependency);
        return id;
    }
    /// <summary>
    /// delete dependency
    /// </summary>
    public void Delete(int id)
    {
        int find=DataSource.Dependencies.RemoveAll(dep => dep.Id == id);
        if(find==0)
           throw new DalDoesNotExistException($"Dependency with ID={id} does Not exist");
    }
    /// <summary>
    /// read dependency by id
    /// </summary>
    public Dependency? Read(int id)
    {

        Dependency? dep = DataSource.Dependencies.FirstOrDefault(item => item.Id == id);
        if(dep==null)  
            throw new DalDoesNotExistException($"Dependency with ID={id} does Not exist");
        return dep;
    }
    /// <summary>
    /// read a dependency according to a certain condition
    /// </summary>
    public Dependency? Read(Func<Dependency, bool> filter)
    {
        Dependency? dep=DataSource.Dependencies.FirstOrDefault(filter);
        if (dep == null)
            throw new DalDoesNotExistException("This dependency does Not exist");
        return dep;
    }
    /// <summary>
    /// read all dependencies
    /// </summary>
    public IEnumerable<Dependency?> ReadAll(Func<Dependency?, bool>? filter = null) //stage 2
    {
        if (filter == null)
            return DataSource.Dependencies.Select(item => item);
        else
            return DataSource.Dependencies.Where(filter);
    }
    /// <summary>
    /// update dependency
    /// </summary>
    public void Update(Dependency item)
    {
        int find=  DataSource.Dependencies.RemoveAll(dep=>dep.Id == item.Id);
        if (find == 0) 
            throw new DalDoesNotExistException($"Dependency with ID={item.Id} does Not exist");
        else
            DataSource.Dependencies.Add(item);
    }
}
