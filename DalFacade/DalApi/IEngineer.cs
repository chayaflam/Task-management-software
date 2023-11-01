

namespace DalApi;

using DO;

public interface IEngineer
{
    /// <summary>
    /// Creates new entity object in DAL
    /// </summary>
    /// <param name="item">the engineer that you  want to create</param>
    /// <returns>engineer id</returns>
    int Create(Engineer item);
    /// <summary>
    /// Reads entity object by its ID 
    /// </summary>
    /// <param name="id">the id of the engineer that you want to read</param>
    /// <returns>the object you ask</returns>
    Engineer? Read(int id);
    /// <summary>
    ///  stage 1 only,Reads all entity objects
    /// </summary>
    /// <returns>return the engineers list</returns>
    List<Engineer> ReadAll();
    /// <summary>
    /// Updates entity object
    /// </summary>
    /// <param name="item">the engineer that you want to update</param>
    void Update(Engineer item);
    /// <summary>
    /// Deletes an object by its Id
    /// </summary>
    /// <param name="id">the id of the engineer that you want to delete</param>
    void Delete(int id); 

}
