

namespace DO;

public record Dependency
    (
    int id,
    int DependentTask,
    int DependsOnTask
    )
{
}
