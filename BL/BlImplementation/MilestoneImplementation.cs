using BlApi;
using BO;
using DalApi;
using DO;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace BlImplementation;
/// <summary>
/// Realizing the functions of the milestone
/// </summary>
internal class MilestoneImplementation :IMilestone
{
    private DalApi.IDal _dal = DalApi.Factory.Get;
    /// <summary>
    /// Creating a project schedule
    /// </summary>
    /// <param name="startProject">Project start time</param>
    /// <param name="finishProject">Project completion time</param>
    /// <returns>List of dependencies between milestones and tasks</returns>
    public IEnumerable<BO.Milestone> CreateSchedule(DateTime startProject,DateTime finishProject)
    {
        //IEnumerable< DO.Dependency?> allDependencies = _dal.Dependency.ReadAll();
        // var dependenciesList = from DO.Dependency doDep in allDependencies
        //                        group doDep by doDep.DependsOnTask into depList
        //                        orderby depList.Key
        //                        select new { key = depList.Key, value = depList.ToList() };

        // dependenciesList=dependenciesList.Distinct();
        return null;
    }
    /// <summary>
    /// Calling for the desired milestone
    /// </summary>
    /// <param name="id">ID of the desired milestone</param>
    /// <returns>desired milestone</returns>
    /// <exception cref="BlDoesNotExistException">The milestone does not exist in the system</exception>
    public BO.Milestone? Read(int id)
    {
        DO.Task? doMilestone = _dal.Task.Read(id);
        if (doMilestone == null)
        {
            throw new BlDoesNotExistException($"milestone with id={id} does not exist");
        }
        BO.Milestone boMilestone = new BO.Milestone
        {
            Id = id,
            Description = doMilestone.Description,
            Alias = doMilestone.Alias,
            CreatedAtDate = doMilestone.CreatedAt,
            Status = (BO.Status)setStatus(id),
            startDate = doMilestone.Start,
            ForecastDate = doMilestone.ForecastDate,
            DeadlineDate = doMilestone.Deadline,
            CompleteDate = doMilestone.Complete,
            CompletionPercentage = null,
            Remarks = doMilestone.Remarks,
            Dependencies = null
        };

        IEnumerable<DO.Task?> allTasks = _dal.Task.ReadAll();
        IEnumerable<BO.TaskInList> milestoneDependency = from DO.Dependency doDep in _dal.Dependency.ReadAll()
                                                         where doDep.DependentTask == id
                                                         select new BO.TaskInList
                                                         {
                                                             Id = doDep.DependsOnTask,
                                                             Description = allTasks.FirstOrDefault(task => task!.Id == doDep.DependsOnTask)!.Description,
                                                             Alias = allTasks.FirstOrDefault(task => task!.Id == doDep.DependsOnTask)!.Alias,
                                                             Status = (BO.Status)setStatus(doDep.DependsOnTask)
                                                         };
        int countMilstone = milestoneDependency.Count();
        int doneTasksCount = 0;

        foreach (var milestone in milestoneDependency)
            if ((int)milestone.Status == 4)
                doneTasksCount++;

        boMilestone.CompletionPercentage = (doneTasksCount / countMilstone) * 100;
        boMilestone.Dependencies = milestoneDependency;
        return boMilestone;
    }
    /// <summary>
    /// Update desired milestone
    /// </summary>
    /// <param name="boMilestone">desired BO milestone object</param>
    /// <exception cref="BlDoesNotExistException">The milestone does not exist in the system</exception>
    public void Update(BO.Milestone boMilestone)
    {
        DO.Task? doMilestone=_dal.Task.Read(boMilestone.Id);
        DO.Task? doMilstoneNew= doMilestone with {Alias=boMilestone.Alias,
            Description=boMilestone.Description,Remarks=boMilestone.Remarks };
        try { 
            _dal.Task.Update(doMilstoneNew);
        }
        catch (DalDoesNotExistException ex)
        {
            throw new BlDoesNotExistException($"Milstone with ID={boMilestone.Id} does Not exist", ex);
        }
    }

    /// <summary>
    /// Calculating the state of a milestone
    /// </summary>
    /// <param name="id">ID of the desired milestone</param>
    /// <returns>The state of the milestone after calculation</returns>
    /// <exception cref="BO.BlDoesNotExistException">The milestone does not exist in the system</exception>
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
  
}
