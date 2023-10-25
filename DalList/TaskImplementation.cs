namespace Dal;
using DalApi;
using DO;
using System.Collections.Generic;

public class TaskImplementation : ITask
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
        Task? deleteTask = DataSource.Tasks.Find(delTask => delTask.Id == id);
        if (deleteTask != null)
        {
            DataSource.Tasks.Remove(deleteTask);
        }
        else throw new Exception($"Task with ID={id} does Not exist");
    }

    public Task? Read(int id)
    {
        return DataSource.Tasks.Find(item => item.Id == id);
    }

    public List<Task> ReadAll()
    {
        return new List<Task>(DataSource.Tasks);
    }

    public void Update(Task item)
    {
       Task? updateTask= DataSource.Tasks.Find(upTask => upTask.Id == item.Id);
        if (updateTask != null)
        {
            DataSource.Tasks.Remove(updateTask);
            DataSource.Tasks.Add(updateTask);
        }
        else { throw new Exception($"Task with ID={item.Id} does Not exist"); }
    }
}
