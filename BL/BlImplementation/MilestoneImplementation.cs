using BlApi;
using DalApi;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace BlImplementation;

internal class MilestoneImplementation :IMilestone
{
    private DalApi.IDal _dal = Factory.Get;
    public IEnumerable<BO.Milestone> Create(IEnumerable<BO.Task> tasks)
    {
        
    }

    public BO.Milestone? Read(int id)
    {
         DO.Task? doMilestone = _dal.Task.Read(id);
        if (doMilestone!.Milestone == false)
            throw new BL();
            
       IEnumerable<TaskInList>milstoneDependency=from DO.Dependency doDependencies in _dal.Dependency.
                                                 ReadAll(dep=>dep!.DependentTask==id)
                  
                                                 select new TaskInList
                                                 {
                                                     Id = id,
                                                     Description = task.Description
                                                 }
    }
    //(from DO.Engineer doEngineer in _dal.Engineer.ReadAll()
    //            select new BO.Engineer()
    //            {
    //                Id = doEngineer.Id,
    //                Name = doEngineer.Name!,
    //                Email = doEngineer.Email!,
    //                Level = (BO.EngineerExperience) doEngineer.Level!,
    //                Cost = (double)doEngineer.Cost!,
    //                Task = (BO.TaskInEngineer)(from DO.Task doTask in _dal.Task.ReadAll()
    //                                           where doTask.EngineerId == doEngineer.Id
    //                                           select new BO.TaskInEngineer()
    //                                           {
    //                                               Id = doTask.Id,
    //                                               Alias = doTask.Alias
    //                                           })
    //            }
    //            );
    public void Update(BO.Milestone item)
    {
        
    }
}
