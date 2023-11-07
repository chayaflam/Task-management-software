namespace DalTest;
using DalApi;
using DO;
using System.Threading.Tasks;

public static class Initialization
{
    private static IDal? s_dal;
    private static readonly Random s_random = new ();
    /// <summary>
    /// create tasks list
    /// </summary>
    private static void createTask()
    {
        IEnumerable<Engineer> engineers = s_dal!.Engineer.ReadAll()!;

        for (int i = 0; i < 100; i++)
        {
            DateTime startDate = DateTime.Now;
            DateTime deadline = startDate.AddDays(40);
            DateTime endDate = startDate.AddDays(s_random.Next(10, 40));
            int engId = engineers.ElementAt(s_random.Next(0, engineers.Count())).Id;
            EngineerExperience level = (EngineerExperience)s_random.Next(0, 4);
            DO.Task newTask = new("fun task", "task alias", true, null,
                startDate, null, deadline, endDate, null, null, engId, level);
            s_dal!.Task.Create(newTask);
        }
    }
    /// <summary>
    /// create engineers list
    /// </summary>
    private static void createEngineer()
    {
        string[] engineersName =
        {
            "Ari",
            "Dani",
            "Avi",
            "Levi",
            "Ben",
            "Yos",
            "Gad",
            "bon",
            "Bob",
            "Nan",
            "Chen",
            "Shay",
            "Chaya",
            "Adina",
            "Lui",
            "Tova",
            "Nani",
            "Lili",
            "Eli",
            "Shimi",
            "Shevi",
            "Gili",
            "Dini",
            "Yafa",
            "Yael",
            "David",
            "Dov",
            "Riki",
            "Rachel",
            "Ron",
            "Ris",
            "Shosh",
            "Gil",
            "Bil",
            "Sam",
            "Smit",
            "Shali",
            "Tami",
            "Rut",
            "Noy",
            "Noa",
            "Hadas"
        };
        foreach (var eng in engineersName)
        {
            int id;
            do
                id = s_random.Next(100000000, 999999999);
            while (s_dal!.Engineer.Read(id) != null);
            string email = $"{eng}@gmail.com";
            EngineerExperience level = (EngineerExperience)s_random.Next(0, 4);
            int cost=s_random.Next(100, 500);
            Engineer newEngineer=new(id,eng,email,level,cost);
            s_dal!.Engineer.Create(newEngineer);
            
        }
    }
    /// <summary>
    /// create dependencys list
    /// </summary>
    private static void createDependency()
    {
        IEnumerable<DO.Task> tasks = s_dal!.Task.ReadAll()!;
        for (int i = 0; i < 250; i++)
        {
            int taskId = tasks.ElementAt(s_random.Next(0, tasks.Count())).Id;
            int dependOnTask = tasks.ElementAt(s_random.Next(0, tasks.Count())).Id;
            Dependency newDep = new(taskId, dependOnTask);
            s_dal!.Dependency.Create(newDep);
        }
    }
    /// <summary>
    /// Initializes the lists
    /// </summary>
    /// <param name="dalDependency">Dependency interface</param>
    /// <param name="dalEngineer">Engineer interface</param>
    /// <param name="dalTask">Task interface</param>
    public static void Do(IDal dal)
    {
        s_dal = dal ?? throw new NullReferenceException("DAL object can not be null!"); //stage 2
        createEngineer();
        createTask();
        createDependency();
    }
}
