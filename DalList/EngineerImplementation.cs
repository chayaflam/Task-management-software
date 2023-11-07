namespace Dal;
using DalApi;
using DO;
using System.Collections.Generic;

/// <summary>
/// Engineer implementation
/// </summary>

internal class EngineerImplementation : IEngineer
{
    public int Create(Engineer item)
    {
        Engineer? eng= DataSource.Engineers.FirstOrDefault(eng=>eng.Id == item.Id);
        if(eng == null)
        {
            DataSource.Engineers.Add(item);
            return item.Id;
        }
        throw new Exception($"Engineer with ID={item.Id} already exists");

    }

    public void Delete(int id)
    {
        int ?find = DataSource.Engineers.RemoveAll(Eng => Eng.Id == id);
        if (find == null)
            throw new Exception($"Engineer with ID={id} does Not exist");
    }

    public Engineer? Read(int id)
    {
        return DataSource.Engineers.FirstOrDefault(item => item.Id == id);
    }

    public Engineer? Read(Func<Engineer, bool> filter)
    {
        return DataSource.Engineers.FirstOrDefault(filter);
    }

    public IEnumerable<Engineer?> ReadAll(Func<Engineer?, bool>? filter = null) //stage 2
    {
        if (filter == null)
            return DataSource.Engineers.Select(item => item);
        else
            return DataSource.Engineers.Where(filter);
    }

    public void Update(Engineer item)
    {
        int? find = DataSource.Engineers.RemoveAll(eng =>eng.Id == item.Id);
        if (find == null) throw new Exception($"Engineer with ID={item.Id} does Not exist");
        else
            DataSource. Engineers.Add(item);

    }
}
