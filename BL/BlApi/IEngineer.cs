

namespace BlApi;

public interface IEngineer
{
    IEnumerable<BO.Engineer?> ReadAll(Func<BO.Engineer?, bool>? filter = null);
    BO.Engineer? Read(int id);
    int Create(BO.Engineer engineer);
    void Delete(int id);
    void Update(BO.Engineer item);
}
