

using DalApi;
using System.Xml.Serialization;

namespace Dal;

internal class TaskImplementation : ITask
{
    public int Create(DO.Task item)
    {
        const string FILETASK = @"..\xml\tasks.xml";
        XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Task>));
        StreamReader reader = new(FILETASK);
        var taskList= (List<Task>?)xmlSerializer?.Deserialize(reader);
        int id = DataSource.Config.NextTaskId;
        //DO.Task task = item with { Id = id };
        //DataSource.Tasks.Add(task);
        //return id;
    }

    public void Delete(int id)
    {
        throw new NotImplementedException();
    }

    public DO.Task? Read(int id)
    {
        throw new NotImplementedException();
    }

    public DO.Task? Read(Func<DO.Task, bool> filter)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<DO.Task?> ReadAll(Func<DO.Task?, bool>? filter = null)
    {
        throw new NotImplementedException();
    }

    public void Update(DO.Task item)
    {
        throw new NotImplementedException();
    }
}
