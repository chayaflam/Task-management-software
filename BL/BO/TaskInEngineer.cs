

namespace BO;

public class TaskInEngineer
{
    public int Id { get; init; }
    public string? Alias { get; set; }
   public override string ToString() => Tools<BO.TaskInEngineer>.ToStringProperty(this);
}

