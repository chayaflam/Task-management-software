
namespace BlApi;
public interface IMilestone
{
    BO.Milestone? Read(int id);
    IEnumerable<BO.Milestone> Create(IEnumerable<BO.Task> tasks);
    void Update(BO.Milestone item);
}
