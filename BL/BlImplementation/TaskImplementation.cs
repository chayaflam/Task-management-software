using BlApi;
using System.Data;

namespace BlImplementation;
internal class TaskImplementation : ITask
{
    private DalApi.IDal _dal = Factory.Get;
    public int Create(BO.Task boTask)
    {
        if (boTask.Id <= 0 || boTask.Alias == "")
            throw new Exception();
        DO.Task doTask = ConvertTaskFromBOtoDO(boTask);
        DO.Dependency newDep=new DO.Dependency(boTask.Milestone.Id, boTask.Id);
        _dal.Dependency.Create(newDep);
        
        foreach (var dep in boTask.Dependencies)
        {
            newDep=new DO.Dependency(dep.Id,boTask.Id);
            _dal.Dependency.Create(newDep);
        }
        try
        {
           return  _dal.Task.Create(doTask);
        }
        catch (Exception ex)
        {
            throw new Exception();
        }
    }

    public void Delete(int id)
    {
       
    }

    public BO.Task? Read(int id)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<BO.Task?> ReadAll(Func<BO.Task?, bool>? filter = null)
    {
        throw new NotImplementedException();
    }

    public void Update(BO.Task item)
    {
        throw new NotImplementedException();
    }
    private DO.Task ConvertTaskFromBOtoDO(BO.Task boTask)
    {
        DO.Task doTask = new DO.Task
         (
         boTask.Description!, boTask.Alias!,
         boTask.Milestone is null?false:true,boTask.CreatedAtDate, boTask.RequiredEffortTime,
         boTask.StartDate,boTask.ScheduledDate,boTask.DeadlineDate,boTask.CompleteDate,boTask.Deliverables,
         boTask.Remarks,boTask.Engineer.Id,
        (DO.EngineerExperience) boTask.Copmlexity);
        return doTask;
    }
    private BO.Task ConvertTaskFromDOtoBO(DO.Task doTask)
    {

        BO.Task boTask = new BO.Task
        {
            Id = doTask.Id,
            Description = doTask.Description,
            Alias = doTask.Alias,
            CreatedAtDate = doTask.CreatedAt,
            Status = setStatus(doTask.Id),
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
        IEnumerable<DO.Task>allTasks=_dal.Task.ReadAll()!;
        IEnumerable<BO.TaskInList> Dependencies = from DO.Dependency doDependency in _dal.Dependency.ReadAll()
                                                     where (doDependency.DependsOnTask == doTask.Id)
                                                     select new BO.TaskInList
                                                     {
                                                         Id = doDependency.DependentTask,
                                                         Description = allTasks.FirstOrDefault(task => task.Id == doDependency.DependentTask)!.Description,
                                                         Alias= allTasks.FirstOrDefault(task => task.Id == doDependency.DependentTask)!.Alias,
                                                         Status=setStatus( allTasks.FirstOrDefault(task => task.Id == doDependency.DependentTask)!.Id)
                                                     };

      IEnumerable< BO.MilestoneInTask> milestone = from DO.Dependency doDependency in _dal.Dependency.ReadAll()
                                       where (doDependency.DependentTask == doTask.Id
                                       && allTasks.FirstOrDefault(task => task.Id == doDependency.DependentTask)!.Milestone)
                                       select new BO.MilestoneInTask
                                       {
                                           Id= doDependency.DependentTask,
                                           Alias = allTasks.FirstOrDefault(task => task.Id == doDependency.DependsOnTask)!.Alias
                                       };
        

                                                       


        return boTask;
    }
   
  
        private BO.Status setStatus(int id)
        {
            try
            {
                DO.Task doTask = _dal.Task.Read(id);
                int status = 0;
                if (doTask.Complete is not null)
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
    

