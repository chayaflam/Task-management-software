namespace DalTest;
using DalApi;
using DO;



public static class Initialization
{
    private static ITask? s_dalTask;
    private static IEngineer? s_dalEngineer;
    private static IDependency? s_dalDependency;
    private static readonly Random s_random = new ();
    private static void createTask()
    {
        List<Engineer> engineers = s_dalEngineer!.ReadAll();

        for (int i = 0; i < 100; i++)
        {
            DateTime startDate = DateTime.Now;
            DateTime deadline = startDate.AddDays(40);
            DateTime endDate = startDate.AddDays(s_random.Next(10, 40));
            int engId = engineers[s_random.Next(engineers.Count)].Id;
            EngineerExperience level = (EngineerExperience)s_random.Next(0, 4);
            Task newTask = new("fun task", "task alias", true, null,
                startDate, null, deadline, endDate, null, null, engId, level);
            s_dalTask!.Create(newTask);
        }
    }
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
            while (s_dalEngineer!.Read(id) != null);
            string email = $"{eng}@gmail.com";
            EngineerExperience level = (EngineerExperience)s_random.Next(0, 4);
            int cost=s_random.Next(100, 500);
            Engineer newEngineer=new(id,eng,email,level,cost);
            s_dalEngineer!.Create(newEngineer);
            
        }
    }

    private static void createDependency()
    {
        List<Task> tasks = s_dalTask!.ReadAll();
        for (int i = 0; i < 250; i++)
        {
            int taskId = tasks[s_random.Next(tasks.Count)].Id;
            int dependOnTask = tasks[s_random.Next(tasks.Count)].Id;
            Dependency newDep = new(taskId, dependOnTask);
            s_dalDependency!.Create(newDep);
        }
    }
    public static void Do(IDependency? dalDependency, IEngineer? dalEngineer, ITask ? dalTask)
    {
        s_dalDependency = dalDependency ?? throw new NullReferenceException("DAL can not be null!");
        s_dalEngineer = dalEngineer ?? throw new NullReferenceException("DAL can not be null!");
        s_dalTask = dalTask ?? throw new NullReferenceException("DAL can not be null!");
        createDependency();
        createEngineer();
        createTask();
    }
}
