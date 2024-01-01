
namespace BlApi;
public interface IMilestone
{
    BO.Milestone? Read(int id);
    IEnumerable<BO.Milestone> CreateSchedule(DateTime startProject, DateTime finishProject);
    void Update(BO.Milestone milestone);
}
