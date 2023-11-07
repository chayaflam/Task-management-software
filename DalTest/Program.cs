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
        static readonly IDal s_dal = new DalList(); //stage 2
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
                    throw new DalInvalidSelectionException("Invalid number");
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
                    throw new DalInvalidSelectionException("Invalid number");
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
                    throw new DalInvalidSelectionException("Invalid number");
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
            s_dal!.Engineer.Create(engineer);
        }
        /// <summary>
        /// read engineer
        /// </summary>
        static void readEngineer()
        {
            Console.WriteLine("enter engineer id");
            int engId;
            int.TryParse(Console.ReadLine()!, out engId);
            Engineer? engineer = s_dal!.Engineer.Read(engId);
            Console.WriteLine(engineer);
        }
        /// <summary>
        /// Read all engineers
        /// </summary>
        static void ReadAllEngineer()
        {
            IEnumerable<Engineer>? engineer = s_dal!.Engineer.ReadAll()!;
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
            Console.WriteLine("enter the engineer id");
            int EngId;
            int.TryParse(Console.ReadLine()!, out EngId);
            Engineer? engineer = s_dal!.Engineer.Read(EngId);
            if (engineer == null) 
            { throw new DalDoesNotExistException($"Engineer with ID={EngId} does Not exist");};
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
            Engineer engineer1 = new(EngId , (string.IsNullOrEmpty(name)?engineer.Name:name),
                (string.IsNullOrEmpty(Email) ? engineer.Email : Email), 
                (level==0?engineer.Level:level),
                (Engcost==0)?engineer.Cost:Engcost);
            s_dal!.Engineer.Update(engineer1);
        }
        /// <summary>
        /// delete engineer
        /// </summary>
        static void deleteEngineer()
        {
                Console.WriteLine("enter engineer id");
                int engId;
                int.TryParse(Console.ReadLine()!, out engId);
                s_dal!.Engineer.Delete(engId);
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
            Console.WriteLine("The Id is: " + s_dal!.Task.Create(task));
        }
        /// <summary>
        /// read task
        /// </summary>
        static void readTask()
        {
            Console.WriteLine("enter the task id");
            int taskId;
            int.TryParse(Console.ReadLine()!, out taskId);
            Console.WriteLine(s_dal!.Task.Read(taskId));
        }
        /// <summary>
        /// Read all task
        /// </summary>
        static void ReadAllTask()
        {
            IEnumerable<DO.Task>? tasks = s_dal!.Task.ReadAll()!;
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
            Console.WriteLine("enter the task id");
            int taskId;
            int.TryParse(Console.ReadLine()!, out taskId);
            DO.Task ?task = s_dal.Task.Read(taskId)!;
            if (task == null) 
            { throw new DalDoesNotExistException($"Engineer with ID={taskId} does Not exist"); };
            Console.WriteLine("enter task Description");
            string description = Console.ReadLine()!;
            Console.WriteLine("enter task Alias");
            string alias = Console.ReadLine()!;
            Console.WriteLine("enter task Milestone");
            bool milestone;
            bool.TryParse(Console.ReadLine()!, out milestone);
            Console.WriteLine("enter task StartDate");
            DateTime StartDate;
            DateTime.TryParse(Console.ReadLine()!, out StartDate);
            Console.WriteLine("enter task ForecastDate");
            DateTime forecastDate;
            DateTime.TryParse(Console.ReadLine()!, out forecastDate);
            Console.WriteLine("enter task Deadline");
            DateTime deadline;
            DateTime.TryParse(Console.ReadLine()!, out deadline);
            Console.WriteLine("enter task CompleteDate");
            DateTime CompleteDate;
            DateTime.TryParse(Console.ReadLine()!, out CompleteDate);
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
            DO.Task newTsk = new((string.IsNullOrEmpty(description) ? task.Description : description),
                (string.IsNullOrEmpty(alias) ? task.Alias : alias),
                milestone ? true : milestone, task.CreatedAt,
                (StartDate.Year == 0001 ? task.Start : StartDate),
                (forecastDate.Year == 0001 ? task.ForecastDate : forecastDate),
                (deadline.Year == 0001 ? task.Deadline : deadline),
                (CompleteDate.Year == 0001 ? task.Complete : CompleteDate),
                (string.IsNullOrEmpty(deliverables) ? task.Deliverables : deliverables),
                (string.IsNullOrEmpty(remarks) ? task.Remarks : remarks),
                (engineerId == 0 ? task.EngineerId : engineerId),
                (complexityLevel == 0 ? task.ComplexityLevel : complexityLevel));
            newTsk.Id = taskId;
            s_dal!.Task.Update(newTsk);
        }
        /// <summary>
        /// delete task
        /// </summary>
        static void deleteTask()
        {

                Console.WriteLine("enter task id");
                int taskId;
                int.TryParse(Console.ReadLine()!, out taskId);
                s_dal!.Task.Delete(taskId);
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
            Console.WriteLine("The Id is: " + s_dal!.Dependency.Create(dependency));

        }
        /// <summary>
        /// read dependency
        /// </summary>
        static void readDep()
        {
            Console.WriteLine("enter dependency task id");
            int  depId;
            int.TryParse(Console.ReadLine()!, out depId);
            Dependency ?dependency= s_dal!.Dependency.Read(depId);
            Console.WriteLine(dependency);
        }
        /// <summary>
        /// Read all dependency
        /// </summary>
        static void ReadAllDep()
        {
            IEnumerable<Dependency>? dependency = s_dal!.Dependency.ReadAll()!;
            foreach (Dependency dep in dependency)
            {
                Console.WriteLine(dep);
            }
        }
        /// <summary>
        /// Update dependency
        /// </summary>
        static void UpdateDep()
        {
            Console.WriteLine("enter dependency task id");
            int depId;
            int taskId;
            int depTaskId;
            int.TryParse(Console.ReadLine(), out depId);
            Dependency ?dependency = s_dal!.Dependency.Read(depId);
            if (dependency == null)
            { 
                throw new DalDoesNotExistException($"Engineer with ID={depId} does Not exist");
            };
            Console.WriteLine("enter task id");
            int.TryParse(Console.ReadLine()!, out taskId);
            Console.WriteLine("enter task depended id");
            int.TryParse(Console.ReadLine(), out depTaskId);

            Dependency newDep=new((taskId != 0)? taskId : dependency.DependentTask, 
                (depTaskId != 0) ? depTaskId : dependency.DependsOnTask);
            newDep.Id = depId;
            s_dal!.Dependency.Update(newDep);
        }
        /// <summary>
        /// delete dependency
        /// </summary>
        static void deleteDep()
        {
                Console.WriteLine("enter dependency task id");
                int depId;
                int.TryParse(Console.ReadLine()!, out depId);
                s_dal!.Dependency.Delete(depId);
        }
        /// <summary>
        /// the main program
        /// </summary>
        
        static void Main(string[] args)
        {
            try
            {
                Initialization.Do(s_dal);
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
                            throw new DalInvalidSelectionException("Invalid number");
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