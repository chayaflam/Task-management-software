using BlApi;
using BO;
using DalApi;
using DO;
using System.Data;
using System.Threading.Tasks;

namespace BlImplementation;
/// <summary>
/// Realizing the functions of the task
/// </summary>
internal class TaskImplementation : BlApi.ITask
{
    private DalApi.IDal _dal = DalApi.Factory.Get;
    /// <summary>
    /// Create DO task from BO task object
    /// </summary>
    /// <param name="boTask">BO task object</param>
    /// <returns>task id</returns>
    /// <exception cref="BO.BlInvalidValuesException">Invalid values entered</exception>
    public int Create(BO.Task boTask)
    {
        if (boTask.Alias == ""|| boTask.CreatedAtDate>boTask.DeadlineDate||boTask.StartDate>boTask.CompleteDate)
            throw new BO.BlInvalidValuesException("Invalid values");
        DO.Task doTask = ConvertTaskFromBOtoDO(boTask);
        return _dal.Task.Create(doTask);
    }
    /// <summary>
    /// Deleting an task from the system
    /// </summary>
    /// <param name="id">Id of the requested task</param>
    /// <exception cref="BlNotErasableException">The task is indelible</exception>
    /// <exception cref="BlDoesNotExistException">The requested task does not exist in the system</exception>
    public void Delete(int id)
    {
        var allDependencies = _dal.Dependency.ReadAll().FirstOrDefault(dep => dep!.DependsOnTask == id)!;
        if (allDependencies != null)
            throw new BO.BlNotErasableException($"Task with ID={id}cannot be deleted");
        try
        {
            _dal.Task.Delete(id);
        }
        catch (DalDoesNotExistException ex)
        {
            throw new BlDoesNotExistException($"task with id={id} does not exist", ex);
        }
    }
    /// <summary>
    ///  Reading task data from DO
    /// </summary>
    /// <param name="id">Id of the requested task</param>
    /// <returns>the requested task</returns>
    /// <exception cref="BlDoesNotExistException">The requested task does not exist in the system</exception>
    public BO.Task? Read(int id)
    {

        DO.Task? doTask = _dal.Task.Read(id);
        if (doTask == null)
            throw new BlDoesNotExistException($"task with id={id} does not exist");

        BO.Task boTask = ConvertTaskFromDOtoBO(doTask);
        return boTask;
    }
    /// <summary>
    /// Reading all or part of the tasks from DO
    /// </summary>
    /// <param name="filter">A condition for calling an task--optional</param>
    /// <returns>The list of tasks meeting the condition</returns>
    public IEnumerable<BO.Task?> ReadAll(Func<DO.Task?, bool>? filter = null)
    {
        IEnumerable<BO.Task> allTask = (from DO.Task doTask in _dal.Task.ReadAll(filter)
                                        select ConvertTaskFromDOtoBO(doTask));
        return allTask;

    }

    /// <summary>
    /// Update data to the requested task
    /// </summary>
    /// <param name="boTask">BO task object</param>
    /// <exception cref="BO.BlInvalidValuesException">Invalid values entered</exception>
    /// <exception cref="BlDoesNotExistException">The requested task does not exist in the system</exception>
    public void Update(BO.Task boTask)
    {
        if (boTask.Id <= 0 || boTask.Alias == "")
            throw new BO.BlInvalidValuesException("Invalid values");
        try {
            checkValidDates(boTask);
            TaskEngineerCanUpdated(boTask);
            DO.Engineer? engineer = _dal.Engineer.Read(boTask!.Engineer!.Id);
            if (engineer == null&& boTask!.Engineer!.Id!=0)
                throw new BO.BlDoesNotExistException($"engineer with ID={boTask.Engineer.Id} does Not exist");
            DO.Task doTask = ConvertTaskFromBOtoDO(boTask);
            _dal.Task.Update(doTask);
        }
        catch (DalDoesNotExistException ex)
        {
            throw new BlDoesNotExistException($"Task with ID={boTask.Id} does Not exist", ex);
        }
    }
    /// <summary>
    /// Convert BO Task object to DO Task object
    /// </summary>
    /// <param name="boTask">BO task object</param>
    /// <returns>The resulting object is converted to DO</returns>
    private DO.Task ConvertTaskFromBOtoDO(BO.Task boTask)
    {
        DO.Task doTask = new DO.Task
         (

         boTask.Description!, boTask.Alias!,
         boTask.Milestone is null ? false : true, boTask.CreatedAtDate, boTask.RequiredEffortTime,
         boTask.StartDate, boTask.ScheduledDate, boTask.ForecastDate, boTask.DeadlineDate, boTask.CompleteDate, boTask.Deliverables,
         boTask.Remarks, boTask.Engineer!.Id,
        (DO.EngineerExperience)boTask.Copmlexity);
        return doTask with { Id = boTask.Id };
    }
    /// <summary>
    /// Convert DO Task object to BO Task object
    /// </summary>
    /// <param name="doTask">DO task object</param>
    /// <returns>The resulting object is converted to BO</returns>
    private BO.Task ConvertTaskFromDOtoBO(DO.Task doTask)
    {

        BO.Task boTask = new BO.Task
        {
            Id = doTask.Id,
            Description = doTask.Description,
            Alias = doTask.Alias,
            CreatedAtDate = doTask.CreatedAt,
            Status = (BO.Status)setStatus(doTask.Id),
            Dependencies = null,
            Milestone = null,
            RequiredEffortTime = doTask.RequiredEffortTime,
            StartDate = doTask.Start,
            ScheduledDate = doTask.ScheduledDate,
            ForecastDate = doTask.ForecastDate,
            DeadlineDate = doTask.Deadline,
            CompleteDate = doTask.Complete,
            Deliverables = doTask.Deliverables,
            Remarks = doTask.Remarks,
            Engineer = null,
            Copmlexity = (BO.EngineerExperience)doTask.ComplexityLevel
        };
        IEnumerable<DO.Task>? allTasks = _dal.Task.ReadAll()!;
        IEnumerable<BO.TaskInList>? taskDependencies = from DO.Dependency doDependency in _dal.Dependency!.ReadAll()!
                                                       where (doDependency.DependsOnTask == doTask.Id)
                                                       select new BO.TaskInList
                                                       {
                                                           Id = doDependency.DependentTask,
                                                           Description = allTasks.FirstOrDefault(task => task.Id == doDependency.DependentTask)!.Description,
                                                           Alias = allTasks.FirstOrDefault(task => task.Id == doDependency.DependentTask)!.Alias,
                                                           Status = (BO.Status)setStatus(allTasks.FirstOrDefault(task => task.Id == doDependency.DependentTask)!.Id)
                                                       };
        BO.MilestoneInTask? taskMilestone = (from DO.Dependency doDependency in _dal.Dependency.ReadAll()
                                             where (doDependency.DependentTask == doTask.Id
                                             && allTasks.FirstOrDefault(task => task.Id == doDependency.DependentTask)!.Milestone)
                                             select new BO.MilestoneInTask
                                             {
                                                 Id = doDependency.DependentTask,
                                                 Alias = allTasks.FirstOrDefault(task => task.Id == doDependency.DependsOnTask)!.Alias
                                             }).FirstOrDefault();
        BO.EngineerInTask? taskEngineer = (from DO.Engineer doEngineers in _dal.Engineer.ReadAll()
                                           where doEngineers.Id == doTask.EngineerId
                                           select new BO.EngineerInTask
                                           {
                                               Id = doEngineers.Id,
                                               Name = doEngineers.Name
                                           }).FirstOrDefault();
        boTask.Dependencies = taskDependencies;
        boTask.Milestone = taskMilestone!;
        boTask.Engineer = taskEngineer;
        return boTask;
    }

    /// <summary>
    /// Calculating the state of a task
    /// </summary>
    /// <param name="id">ID of the desired task</param>
    /// <returns>The state of the task after calculation</returns>
    /// <exception cref="BO.BlDoesNotExistException">The requested task does not exist in the system</exception>
    private int setStatus(int id)
    {
        try
        {
            DO.Task? doTask = _dal.Task.Read(id);
            int status = 0;
            if (doTask!.Complete is not null)
                status = 4;
            else if (doTask.ForecastDate < DateTime.Now)
                status = 3;
            else if (doTask.Start < DateTime.Now)
                status = 2;
            else if (doTask.ForecastDate is not null)
                status = 1;
            else status = 0;
            return status;
        }
        catch (DO.DalDoesNotExistException ex)
        {
            throw new BO.BlDoesNotExistException($"Task with Id={id} was not found", ex);
        }
    }
    /// <summary>
    /// The function receives a task and checks if it is possible to update the engineer working on it
    /// </summary>
    /// <param name="BoTask">bo task for checking</param>
    /// <exception cref="BO.BlInvalidValuesException">The engineer is busy with another task</exception>
    private void TaskEngineerCanUpdated(BO.Task BoTask)
    {
        var taskInEngineer = (from DO.Task doTask in _dal.Task.ReadAll()
                                    where doTask.EngineerId == BoTask!.Engineer!.Id &&
                                    doTask.Complete > DateTime.Now
                                    select doTask).FirstOrDefault();
        if (taskInEngineer != null && BoTask!.Id != taskInEngineer.Id)
            throw new BO.BlInvalidValuesException($"The engineer with ID={BoTask!.Engineer!.Id}is busy with another task");
    }
    /// <summary>
    /// The function receives a task and checks if its dates are correct
    /// </summary>
    /// <param name="boTask">bo task for checking</param>
    /// <exception cref="BO.BlInvalidValuesException">Invalid dates values</exception>
    private void checkValidDates(BO.Task boTask)
    {
        if(((boTask.CreatedAtDate > boTask.DeadlineDate|| boTask.StartDate > boTask.DeadlineDate )&& boTask.DeadlineDate!=new DateTime())
            || ((boTask.CreatedAtDate > boTask.CompleteDate || boTask.StartDate > boTask.CompleteDate) && boTask.CompleteDate != new DateTime()))
        {
            throw new BO.BlInvalidValuesException("Invalid dates values");
        }
    }
}
