using Dal;
using DalApi;
using DO;
using Microsoft.VisualBasic.FileIO;
using System;
using System.Reflection.Emit;
using System.Threading.Tasks;
using System.Xml.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;


namespace DalTest
{

    internal class Program
    {
        private static IDependency? s_dalDependency = new DependencyImplementation(); //stage 1
        private static IEngineer? s_dalEngineer = new EngineerImplementation(); //stage 1
        private static ITask? s_dalTask = new TaskImplementation(); //stage 1
        /// <summary>
        /// Prints the actions menu of a particular entity
        /// </summary>
        static void printCRUD()
        {
            Console.WriteLine("Select an action");
            Console.WriteLine("1 exit"+" 2 Create" + " 3 Read" + " 4 ReadAll" + " 5 Update" + " 6 Delete");


        }
        /// <summary>
        /// Dependency CRUD
        /// </summary>
        static void DependencyCRUD()
        {
            printCRUD();
            int choose = Convert.ToInt32(Console.ReadLine());
            switch (choose)
            {
                case 1:
                    return;
                case 2:
                    createDep();
                    break;
                case 3:
                    readDep();
                    break;
                case 4:
                    ReadAllDep();
                    break;
                case 5:
                    UpdateDep();
                    break;
                case 6:
                    deleteDep();
                    break;
                default:
                    throw new Exception("Invalid number");
            }
        }
        /// <summary>
        /// Task CRUD
        /// </summary>
        static void TaskCRUD()
        {
            printCRUD();
            int choose = Convert.ToInt32(Console.ReadLine());
            switch (choose)
            {
                case 1:
                    return;
                case 2:
                    createTask();
                    break;
                case 3:
                    readTask();
                    break;
                case 4:
                    ReadAllTask();
                    break;
                case 5:
                    UpdateTask();
                    break;
                case 6:
                    deleteTask();
                    break;
                default:
                    throw new Exception("Invalid number");
            }
        }
        /// <summary>
        /// Engineer CRUD
        /// </summary>
        static void EngineerCRUD()
        {
            printCRUD();
            int choose = Convert.ToInt32(Console.ReadLine());
            switch (choose)
            {
                case 1: 
                    return;
                case 2:
                    createEngineer();
                    break;
                case 3:
                    readEngineer();
                    break;
                case 4:
                    ReadAllEngineer();
                    break;
                case 5:
                    UpdateEngineer();
                    break;
                case 6:
                    deleteEngineer();
                    break;
                default:
                    throw new Exception("Invalid number");
            }
        }
        /// <summary>
        /// create engineer
        /// </summary>
        static void createEngineer()
        {
            Console.WriteLine("enter the engineer id");
            int EngId;
            int.TryParse(Console.ReadLine()!, out EngId);
            Console.WriteLine("enter the engineer name");
            string name= Console.ReadLine()!;
            Console.WriteLine("enter the engineer Email");
            string Email = Console.ReadLine()!;
            Console.WriteLine("enter engineer level");
            EngineerExperience level;
            EngineerExperience.TryParse(Console.ReadLine()!, out level);
            Console.WriteLine("enter the engineer cost");
            double Engcost;
            double.TryParse(Console.ReadLine()!, out Engcost);
            Engineer engineer = new(EngId, name, Email, level, Engcost);
            s_dalEngineer!.Create(engineer);
        }
        /// <summary>
        /// read engineer
        /// </summary>
        static void readEngineer()
        {
            Console.WriteLine("enter engineer id");
            int engId;
            int.TryParse(Console.ReadLine()!, out engId);
            Engineer? engineer = s_dalEngineer!.Read(engId);
            Console.WriteLine(engineer);
        }
        /// <summary>
        /// Read all engineers
        /// </summary>
        static void ReadAllEngineer()
        {
            List<Engineer>? engineer = s_dalEngineer!.ReadAll();
            foreach (Engineer eng in engineer)
            {
                Console.WriteLine(eng);
            }
        }
        /// <summary>
        /// Update engineer
        /// </summary>
        static void UpdateEngineer()
        {
            try
            {
                Console.WriteLine("enter the engineer id");
                int EngId;
                int.TryParse(Console.ReadLine()!, out EngId);
                Console.WriteLine("enter the engineer name");
                string name = Console.ReadLine()!;
                Console.WriteLine("enter the engineer Email");
                string Email = Console.ReadLine()!;
                Console.WriteLine("enter engineer level");
                EngineerExperience level;
                EngineerExperience.TryParse(Console.ReadLine()!, out level);
                Console.WriteLine("enter the engineer cost");
                double Engcost;
                double.TryParse(Console.ReadLine()!, out Engcost);
                Engineer engineer = new(EngId, name, Email, level, Engcost);
                s_dalEngineer!.Update(engineer);
            }
            catch (Exception ex) { Console.WriteLine(ex); }
        }
        /// <summary>
        /// delete engineer
        /// </summary>
        static void deleteEngineer()
        {
            try
            {
                Console.WriteLine("enter engineer id");
                int engId;
                int.TryParse(Console.ReadLine()!, out engId);
                s_dalEngineer!.Delete(engId);
            }
            catch (Exception ex) { Console.WriteLine(ex); }
        }
        /// <summary>
        /// create task
        /// </summary>
        static void createTask()
        {
            Console.WriteLine("enter task Description");
            string description =Console.ReadLine()!;
            Console.WriteLine("enter task Alias");
            string alias = Console.ReadLine()!;
            Console.WriteLine("enter task Milestone");
            bool milestone;
            bool.TryParse( Console.ReadLine()!,out milestone);
            DateTime createdAt=DateTime.Now;
            Console.WriteLine("enter task ForecastDate");
            DateTime forecastDate;
            DateTime.TryParse(Console.ReadLine()!, out forecastDate);
            Console.WriteLine("enter task Deadline");
            DateTime deadline;
            DateTime.TryParse(Console.ReadLine()!, out deadline);
            Console.WriteLine("enter task Deliverables");
            string deliverables = Console.ReadLine()!;
            Console.WriteLine("enter task Remarks");
            string remarks = Console.ReadLine()!;
            Console.WriteLine("enter task EngineerId");
            int engineerId;
            int.TryParse(Console.ReadLine()!, out engineerId);
            Console.WriteLine("enter task ComplexityLevel");
            EngineerExperience complexityLevel;
            EngineerExperience.TryParse(Console.ReadLine()!, out complexityLevel);
            DO.Task task = new(description, alias, milestone, createdAt,
                null, forecastDate, deadline, null, deliverables, remarks, engineerId, complexityLevel);
            Console.WriteLine("The Id is: " + s_dalTask!.Create(task));
        }
        /// <summary>
        /// read task
        /// </summary>
        static void readTask()
        {
            Console.WriteLine("enter the task id");
            int taskId;
            int.TryParse(Console.ReadLine()!, out taskId);
            Console.WriteLine(s_dalTask!.Read(taskId));
        }
        /// <summary>
        /// Read all task
        /// </summary>
        static void ReadAllTask()
        {
            List<DO.Task>? tasks = s_dalTask!.ReadAll();
            foreach (DO.Task task in tasks)
            {
                Console.WriteLine(task);
            }
        }
        /// <summary>
        /// Update task
        /// </summary>
        static void UpdateTask()
        {
            try { 
            Console.WriteLine("enter the task id");
            int taskId;
            int.TryParse(Console.ReadLine()!, out taskId);

            Console.WriteLine("enter task Description");
            string description = Console.ReadLine()!;
            Console.WriteLine("enter task Alias");
            string alias = Console.ReadLine()!;
            Console.WriteLine("enter task Milestone");
            bool milestone;
            bool.TryParse(Console.ReadLine()!, out milestone);
            DateTime createdAt = DateTime.Now;
            Console.WriteLine("enter task ForecastDate");
            DateTime forecastDate;
            DateTime.TryParse(Console.ReadLine()!, out forecastDate);
            Console.WriteLine("enter task Deadline");
            DateTime deadline;
            DateTime.TryParse(Console.ReadLine()!, out deadline);
            Console.WriteLine("enter task Deliverables");
            string deliverables = Console.ReadLine()!;
            Console.WriteLine("enter task Remarks");
            string remarks = Console.ReadLine()!;
            Console.WriteLine("enter task EngineerId");
            int engineerId;
            int.TryParse(Console.ReadLine()!, out engineerId);
            Console.WriteLine("enter task ComplexityLevel");
            EngineerExperience complexityLevel;
            EngineerExperience.TryParse(Console.ReadLine()!, out complexityLevel);
            DO.Task task = new(description, alias, milestone, createdAt,
                null, forecastDate, deadline, null, deliverables, remarks, engineerId, complexityLevel);
            task.Id = taskId;
            s_dalTask!.Update(task);
            }
            catch (Exception ex) { Console.WriteLine(ex); }
        }
        /// <summary>
        /// delete task
        /// </summary>
        static void deleteTask()
        {
            try
            {
                Console.WriteLine("enter task id");
                int taskId;
                int.TryParse(Console.ReadLine()!, out taskId);
                s_dalTask!.Delete(taskId);
            }
            catch (Exception ex) { Console.WriteLine(ex); }
        }
        /// <summary>
        /// create dependency
        /// </summary>
        static void createDep()
        {
            Console.WriteLine("enter task id");
            int taskId, depTaskId;
            int.TryParse(Console.ReadLine()!, out taskId);
            Console.WriteLine("enter task depended id");

            int.TryParse(Console.ReadLine(), out depTaskId);
            Dependency dependency = new(taskId, depTaskId);
            Console.WriteLine("The Id is: " + s_dalDependency!.Create(dependency));

        }
        /// <summary>
        /// read dependency
        /// </summary>
        static void readDep()
        {
            Console.WriteLine("enter dependency task id");
            int  depId;
            int.TryParse(Console.ReadLine()!, out depId);
            Dependency ?dependency= s_dalDependency!.Read(depId);
            Console.WriteLine(dependency);
        }
        /// <summary>
        /// Read all dependency
        /// </summary>
        static void ReadAllDep()
        {
             s_dalDependency!.ReadAll().ForEach(x => Console.WriteLine(x));
        }
        /// <summary>
        /// Update dependency
        /// </summary>
        static void UpdateDep()
        {
            try {
            Console.WriteLine("enter dependency task id");
            int depId, taskId, depTaskId; ;
            int.TryParse(Console.ReadLine()!, out depId);
            Console.WriteLine("enter task id");
            int.TryParse(Console.ReadLine()!, out taskId);
            Console.WriteLine("enter task depended id");
            int.TryParse(Console.ReadLine(), out depTaskId);
            Dependency? dependency = new(taskId, depTaskId);
            dependency.Id = depId;
            }catch (Exception ex) { Console.WriteLine(ex); }
        }
        /// <summary>
        /// delete dependency
        /// </summary>
        static void deleteDep()
        {
            try
            {
                Console.WriteLine("enter dependency task id");
                int depId;
                int.TryParse(Console.ReadLine()!, out depId);
                s_dalDependency!.Delete(depId);
            }
            catch (Exception ex) { Console.WriteLine(ex); }
        }
        /// <summary>
        /// the main program
        /// </summary>
        
        static void Main(string[] args)
        {
            try
            {
                Initialization.Do(s_dalDependency, s_dalEngineer, s_dalTask);
                do
                {
                    Console.WriteLine("Select an entity");
                    Console.WriteLine(" 0 Exit" + " 1 Dependency " + " 2 Engineer" + " 3 Task");
                    int myChoose;
                    int.TryParse(Console.ReadLine()!, out myChoose);
                    switch (myChoose)
                    {
                        case 0:
                            return;
                        case 1:
                            DependencyCRUD();
                            break;
                        case 2:
                            EngineerCRUD();
                            break;
                        case 3:
                            TaskCRUD();
                            break;
                        default:
                            throw new Exception("Invalid number");
                    }
                } while (true);
            }
           catch(Exception error) 
            { 
                Console.WriteLine(error);
            }
        }
    }
}