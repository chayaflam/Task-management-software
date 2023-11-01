using System.Runtime.CompilerServices;
namespace DO;



/// <summary>
/// Dependency Entity represents a dependency with all its props
/// </summary>
/// <param name="DependentTask">The dependent task</param>
/// <param name="DependsOnTask">The task depends on it</param>
public record Dependency
    (
   
    int DependentTask,
    int DependsOnTask
    )

{
   public int Id;
};
