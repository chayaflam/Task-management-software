

namespace DalApi;
using DO;
public interface IDependency
{/// <summary>
/// Creates new entity object in DAL
/// </summary>
/// <param name="item">the dependency that you  want to create</param>
/// <returns>dependency id</returns>
    int Create(Dependency item);
    /// <summary>
    /// Reads entity object by its ID 
    /// </summary>
    /// <param name="id">the id of the dependency that you want to read</param>
    /// <returns>the object you ask</returns>
    Dependency? Read(int id);
    /// <summary>
    /// stage 1 only, Reads all entity objects
    /// </summary>
    /// <returns>return the dependencys list</returns>
    List<Dependency> ReadAll();
    /// <summary>
    /// Updates entity object
    /// </summary>
    /// <param name="item">the dependency that you want to update</param>
    void Update(Dependency item);
    /// <summary>
    /// Deletes an object by its Id
    /// </summary>
    /// <param name="id">the id of the dependency that you want to delete</param>
    void Delete(int id); 

}
