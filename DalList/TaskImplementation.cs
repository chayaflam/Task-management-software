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
    /// <summary>
    /// create task
    /// </summary>
    public int Create(Task item)
    {
        int id = DataSource.Config.NextTaskId;
        DO.Task task =item with { Id=id,CreatedAt=DateTime.Now};
        DataSource.Tasks.Add(task);
        return id;
    }
    /// <summary>
    /// delete task
    /// </summary>
    public void Delete(int id)
    {
        int find = DataSource.Tasks.RemoveAll(task => task.Id == id);
        if (find == 0)
            throw new DalDoesNotExistException($"Task with ID={id} does Not exist");
    }
    /// <summary>
    /// read task by id
    /// </summary>
    public Task? Read(int id)
    {
        return DataSource.Tasks.FirstOrDefault(item => item.Id == id);
    }
    /// <summary>
    /// read a task according to a certain condition
    /// </summary>
    public Task? Read(Func<Task, bool> filter)
    {
       return DataSource.Tasks.FirstOrDefault(filter);
    }
    /// <summary>
    /// read all tasks
    /// </summary>
    public IEnumerable<Task?> ReadAll(Func<Task?, bool>? filter = null) //stage 2
    {
        if (filter == null)
            return DataSource.Tasks.Select(item => item);
        else
            return DataSource.Tasks.Where(filter);
    }
    /// <summary>
    /// update task
    /// </summary>
    public void Update(Task item)
    {
        int find = DataSource.Tasks.RemoveAll(task => task.Id == item.Id);
        if (find == 0)
            throw new DalDoesNotExistException($"Task with ID={item.Id} does Not exist");
        else
            DataSource.Tasks.Add(item);
    }
}
