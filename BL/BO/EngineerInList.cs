

namespace BO;

public class EngineerInList
{
    public int Id { get; init; }
    public string Name { get; set; }
    public EngineerExperience Level { get; set; }
    public override string ToString() => Tools<BO.EngineerInList>.ToStringProperty(this);
}
