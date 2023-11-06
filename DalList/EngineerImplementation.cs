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
        Engineer? createEng= DataSource.Engineers.Find(eng => eng.Id == item.Id);
        if (createEng == null)
        {
            DataSource.Engineers.Add(item);
            return item.Id;
        }
        throw new Exception($"Engineer with ID={item.Id} already exists");
    }

    public void Delete(int id)
    {
        Engineer? deleteEng = DataSource.Engineers.Find(delEng => delEng.Id == id);
        if (deleteEng != null)
        {
            DataSource.Engineers.Remove(deleteEng);
        }
        else throw new Exception($"Engineer with ID={id} does Not exist");
    }

    public Engineer? Read(int id)
    {
        return DataSource.Engineers.Find(item => item.Id == id);
    }

    public List<Engineer> ReadAll()
    {
        return new List<Engineer>(DataSource.Engineers);
    }

    public void Update(Engineer item)
    {
        Engineer? updateEng = DataSource.Engineers.Find(upEng => upEng.Id == item.Id);
        if (updateEng != null)
        {
            DataSource.Engineers.Remove(updateEng);
            DataSource.Engineers.Add(item);
        }
        else { throw new Exception($"Engineer with ID={item.Id} does Not exist"); }
    }
}
