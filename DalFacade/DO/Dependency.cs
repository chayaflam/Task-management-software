

using System.Runtime.CompilerServices;

namespace DO;

public record Dependency
    (
   
    int DependentTask,
    int DependsOnTask
    )

{
   public int Id;
};
