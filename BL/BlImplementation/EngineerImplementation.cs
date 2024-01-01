
using BlApi;
using System;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Xml;
using System.Data.Common;



namespace BlImplementation;
/// <summary>
/// The realization of the functions of the engineer
/// </summary>
internal class EngineerImplementation : IEngineer
{
    private DalApi.IDal _dal = Factory.Get;
    /// <summary>
    /// Create DO engineer from BO engineer object
    /// </summary>
    /// <param name="boEngineer">BO engineer object</param>
    /// <returns>engineer id</returns>
    /// <exception cref="BO.BlInvalidValuesException">Invalid values entered</exception>
    /// <exception cref="BO.BlAlreadyExistsException">The engineer already exists in the system</exception>
    public int Create(BO.Engineer boEngineer)
    {
        if (boEngineer.Id <= 0 || boEngineer.Name == "" || boEngineer.Name == null ||
           boEngineer.Email == "" || boEngineer.Email == null || boEngineer.Cost <= 0)
            throw new BO.BlInvalidValuesException("invalid values");
        DO.Engineer doEngineer = new DO.Engineer
         (boEngineer.Id,boEngineer.Name,boEngineer.Email,(DO.EngineerExperience?)boEngineer.Level,boEngineer.Cost);
        try
        {
            int idEngineer = _dal.Engineer.Create(doEngineer);
            return idEngineer;
        }
        catch (DO.DalAlreadyExistsException ex)
        {
            throw new BO.BlAlreadyExistsException($"Engineer with ID={boEngineer.Id} already exists", ex);
        }
    }
    /// <summary>
    /// Deleting an engineer from the system
    /// </summary>
    /// <param name="id"> Id of the requested engineer</param>
    /// <exception cref="BO.BlDoesNotExistException">The requested engineer does not exist in the system</exception>
    /// <exception cref="BO.BlNotErasableException">The engineer is indelible</exception>
    public void Delete(int id)
    {
        DO.Engineer? doEngineer = _dal.Engineer.Read(id);
        if (doEngineer == null)
            throw new BO.BlDoesNotExistException($"Engineer with ID={id} does Not exist");

        List<DO.Task?> tasks = (List<DO.Task?>)_dal.Task.ReadAll();
        DO.Task? task = tasks.FirstOrDefault(x => x!.EngineerId == id);
        if (task != null)
        {
            throw new BO.BlNotErasableException($"Engineer with ID={id}cannot be deleted");
        }
        try
        {
            _dal.Engineer.Delete(id);
        }
        catch (DO.DalDoesNotExistException ex)
        {

            throw new BO.BlDoesNotExistException($"Engineer with ID={id} does Not exist",ex);
        }
    }
    /// <summary>
    /// Reading engineer data from DO
    /// </summary>
    /// <param name="id">Id of the requested engineer</param>
    /// <returns>the requested engineer</returns>
    /// <exception cref="BO.BlDoesNotExistException">The requested engineer does not exist in the system</exception>
    public BO.Engineer? Read(int id)
    {
        DO.Engineer? doEngineer = _dal.Engineer.Read(id);
        if (doEngineer == null)
            throw new BO.BlDoesNotExistException($"Engineer with ID={id} does Not exist");

        return new BO.Engineer()
        {
            Id = id,
            Name = doEngineer!.Name!,
            Email = doEngineer!.Email!,
            Level = (BO.EngineerExperience)doEngineer.Level!,
            Cost = (double)doEngineer.Cost!,
        };
    }
    /// <summary>
    /// Reading all or part of the engineers from DO
    /// </summary>
    /// <param name="filter">A condition for calling an engineer--optional</param>
    /// <returns>The list of engineers meeting the condition</returns>
    public IEnumerable<BO.Engineer?> ReadAll(Func<BO.Engineer?, bool>? filter = null)
    {
        return (from DO.Engineer doEngineer in _dal.Engineer.ReadAll((Func<DO.Engineer?, bool>?)filter )
                select new BO.Engineer
                {
                    Id = doEngineer.Id,
                    Name = doEngineer.Name!,
                    Email = doEngineer.Email!,
                    Level = (BO.EngineerExperience)doEngineer.Level!,
                    Cost = (double)doEngineer.Cost!,
                    Task = (BO.TaskInEngineer)(from DO.Task doTask in _dal.Task.ReadAll()
                                               where doTask.EngineerId == doEngineer.Id
                                               select new BO.TaskInEngineer()
                                               {
                                                   Id = doTask.Id,
                                                   Alias = doTask.Alias
                                               })
                }
                );
    }
    /// <summary>
    /// Update data to the requested engineer
    /// </summary>
    /// <param name="boEngineer">BO engineer object</param>
    /// <exception cref="BO.BlInvalidValuesException">Invalid values entered</exception>
    /// <exception cref="BO.BlDoesNotExistException">The requested engineer does not exist in the system</exception>
    public void Update(BO.Engineer boEngineer)
    {
        if (boEngineer.Id <= 0 || boEngineer.Name == "" ||
           boEngineer.Email == "" || boEngineer.Cost <= 0)
            throw new BO.BlInvalidValuesException("invalid values");
        DO.Engineer doEngineer = new DO.Engineer
             (boEngineer.Id, boEngineer.Name, boEngineer.Email, (DO.EngineerExperience?)boEngineer.Level, boEngineer.Cost);
        try
        {
            _dal.Engineer.Update(doEngineer);
        }
        catch (DO.DalDoesNotExistException ex)
        {
            throw new BO.BlDoesNotExistException($"Engineer with ID={boEngineer.Id} does Not exist",ex);
        }
    }
}
