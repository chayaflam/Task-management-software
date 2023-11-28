

using DalApi;
using DO;
using System.Xml.Serialization;

namespace Dal;

internal class TaskImplementation : ITask
{
    const string s_Task = "tasks";
    /// <summary>
    /// create task
    /// </summary>
    public int Create(DO.Task item)
    {
        List<DO.Task> listTask = XMLTools.LoadListFromXMLSerializer<DO.Task>(s_Task);
        int id = Config.NextTaskId;
        DO.Task task = item with { Id = id };
        listTask.Add(task);
        XMLTools.SaveListToXMLSerializer(listTask, s_Task);
        return id;
    }
    /// <summary>
    /// delete task
    /// </summary>
    public void Delete(int id)
    {
        List<DO.Task> listTask = XMLTools.LoadListFromXMLSerializer<DO.Task>(s_Task)!;
        int find = listTask.RemoveAll(task => task.Id == id);
        if (find == 0)
            throw new DalDoesNotExistException($"Task with ID={id} does Not exist");
        XMLTools.SaveListToXMLSerializer(listTask, s_Task);
    }
    /// <summary>
    /// read task by id
    /// </summary>
    public DO.Task? Read(int id)
    {
        List<DO.Task> listTask = XMLTools.LoadListFromXMLSerializer<DO.Task>(s_Task)!;
        return listTask.FirstOrDefault(item => item.Id == id);
    }
    /// <summary>
    /// read a task according to a certain condition
    /// </summary>
    public DO.Task? Read(Func<DO.Task, bool> filter)
    {
        List<DO.Task> listTask = XMLTools.LoadListFromXMLSerializer<DO.Task>(s_Task)!;
        return listTask.FirstOrDefault(filter);
    }
    /// <summary>
    /// read all tasks
    /// </summary>
    public IEnumerable<DO.Task?> ReadAll(Func<DO.Task?, bool>? filter = null)
    {
        List<DO.Task> listTask = XMLTools.LoadListFromXMLSerializer<DO.Task>(s_Task)!;
        if (filter == null)
            return listTask.Select(item => item);
        else
            return listTask.Where(filter);
    }
    /// <summary>
    /// update task
    /// </summary>
    public void Update(DO.Task item)
    {
        List<DO.Task> listTask = XMLTools.LoadListFromXMLSerializer<DO.Task>(s_Task)!;
        int find = listTask.RemoveAll(task => task.Id == item.Id);
        if (find == 0) throw new DalDoesNotExistException($"Task with ID={item.Id} does Not exist");
        else
        {
            listTask.Add(item);
            XMLTools.SaveListToXMLSerializer(listTask, s_Task);
        }

    }
}
