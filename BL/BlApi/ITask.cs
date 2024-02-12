namespace BlApi;
public interface ITask
{
    IEnumerable<BO.Task?> ReadAll(Func<DO.Task?, bool>? filter = null);
    BO.Task? Read(int id);
    int Create(BO.Task engineer);
    void Delete(int id);
    void Update(BO.Task item);
}
