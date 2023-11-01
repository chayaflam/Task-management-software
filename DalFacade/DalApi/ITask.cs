namespace DalApi;
using DO;

public interface ITask
{/// <summary>
/// Creates new entity object in DAL
/// </summary>
/// <param name="item">the task that you  want to create</param>
/// <returns>task id</returns>
    int Create(Task item);
    /// <summary>
    /// Reads entity object by its ID 
    /// </summary>
    /// <param name="id">the id of the task that you want to read</param>
    /// <returns>the object you ask</returns>
    Task? Read(int id);
    /// <summary>
    /// stage 1 only,Reads all entity objects
    /// </summary>
    /// <returns>return the tasks list</returns>
    List<Task> ReadAll();
    /// <summary>
    /// Updates entity object
    /// </summary>
    /// <param name="item">the task that you  want to update</param>
    void Update(Task item);
    /// <summary>
    /// Deletes an object by its Id
    /// </summary>
    /// <param name="id">the id of the task that you want to delete</param>
    void Delete(int id); 

}
