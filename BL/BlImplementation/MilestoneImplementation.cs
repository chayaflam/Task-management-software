using BlApi;
using DalApi;

namespace BlImplementation;

internal class MilestoneImplementation :IMilestone
{
    private DalApi.IDal _dal = Factory.Get;
    public IEnumerable<BO.Milestone> Create(IEnumerable<BO.Task> tasks)
    {
        
    }

    public BO.Milestone? Read(int id)
    {
        IEnumerable<DO.Dependency>
        DO.Task? doMilestone = _dal.Task.Read(id);
        if (doMilestone!.Milestone == false)
            throw new BL();
        
       
        if (doTask == null)
            throw new BO.BlDoesNotExistException($"Engineer with ID={id} does Not exist");
        
    }

    public void Update(BO.Milestone item)
    {
        
    }
}
