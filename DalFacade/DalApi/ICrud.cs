using DO;
namespace DalApi;

public interface ICrud<T> where T : class
{
    /// <summary>
    /// Creates new entity object in DAL
    /// </summary>
    /// <param name="item">the T that you  want to create</param>
    /// <returns>T id</returns>
    int Create(T item);
    /// <summary>
    /// Reads entity object by its ID 
    /// </summary>
    /// <param name="id">the id of the T that you want to read</param>
    /// <returns>the object you ask</returns>
    T? Read(int id);
    /// <summary>
    /// stage 1 only, Reads all entity objects
    /// </summary>
    /// <returns>return the T list</returns>
    IEnumerable<T?> ReadAll(Func<T?, bool>? filter = null); // stage 2
    /// <summary>
    /// Updates entity object
    /// </summary>
    /// <param name="item">the T that you want to update</param>
    void Update(T item);
    /// <summary>
    /// Deletes an object by its Id
    /// </summary>
    /// <param name="id">the id of the T that you want to delete</param>
    void Delete(int id);
    /// <summary>
    /// Returns the first object that returns true for the received function
    /// </summary>
    /// <param name="filter">function</param>
    /// <returns>Returns the first object that returns true for the received function</returns>
    T? Read(Func<T, bool> filter); // stage 2
}
