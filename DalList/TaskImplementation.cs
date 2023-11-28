//namespace Dal;
//using DalApi;
//using DO;
//using System.Collections.Generic;
//using System.Linq;
namespace Dal;
using DalApi;
using DO;
using System.Collections.Generic;
/// <summary>
/// Task implementation
/// </summary>

internal class TaskImplementation : ITask
{
    
    public int Create(Task item)
    {
        int id = DataSource.Config.NextTaskId;
        DO.Task task =item with { Id=id};
        DataSource.Tasks.Add(task);
        return id;
    }

    public void Delete(int id)
    {
        int find = DataSource.Tasks.RemoveAll(task => task.Id == id);
        if (find == 0)
            throw new DalDoesNotExistException($"Task with ID={id} does Not exist");
    }

    public Task? Read(int id)
    {
        return DataSource.Tasks.FirstOrDefault(item => item.Id == id);
    }

    public Task? Read(Func<Task, bool> filter)
    {
       return DataSource.Tasks.FirstOrDefault(filter);
    }

    public IEnumerable<Task?> ReadAll(Func<Task?, bool>? filter = null) //stage 2
    {
        if (filter == null)
            return DataSource.Tasks.Select(item => item);
        else
            return DataSource.Tasks.Where(filter);
    }

    public void Update(Task item)
    {
        int find = DataSource.Tasks.RemoveAll(task => task.Id == item.Id);
        if (find == 0) throw new DalDoesNotExistException($"Task with ID={item.Id} does Not exist");
        else
            DataSource.Tasks.Add(item);
    }
}
