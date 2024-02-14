
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
    private DalApi.IDal _dal = DalApi.Factory.Get;
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
         (boEngineer.Id, boEngineer.Name, boEngineer.Email, (DO.EngineerExperience?)boEngineer.Level, boEngineer.Cost);
        if (boEngineer!.Task!.Id!=0)
        {
            DO.Task? task = _dal.Task.Read(boEngineer.Task.Id) ??
                throw new BO.BlDoesNotExistException($"Task with ID={boEngineer.Task.Id} does Not exist");
            _dal.Task.Update(task with { EngineerId = boEngineer.Id });
        }
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

            throw new BO.BlDoesNotExistException($"Engineer with ID={id} does Not exist", ex);
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
            Task = (from DO.Task doTask in _dal.Task.ReadAll()
                    where doTask.EngineerId == doEngineer.Id&& doTask.Complete==null
                    select new BO.TaskInEngineer()
                    {
                        Id = doTask.Id,
                        Alias = doTask.Alias
                    }).FirstOrDefault()
        };
    }
    /// <summary>
    /// Reading all or part of the engineers from DO
    /// </summary>
    /// <param name="filter">A condition for calling an engineer--optional</param>
    /// <returns>The list of engineers meeting the condition</returns>
    public IEnumerable<BO.Engineer?> ReadAll(Func<DO.Engineer?, bool>? filter = null)
    {
        IEnumerable<BO.Engineer> allEngineers = (from DO.Engineer doEngineer in _dal.Engineer.ReadAll(filter)
                                                 select new BO.Engineer
                                                 {
                                                     Id = doEngineer.Id,
                                                     Name = doEngineer.Name!,
                                                     Email = doEngineer.Email!,
                                                     Level = (BO.EngineerExperience)doEngineer.Level!,
                                                     Cost = (double)doEngineer.Cost!,
                                                     Task = (from DO.Task doTask in _dal.Task.ReadAll()
                                                             where doTask.EngineerId == doEngineer.Id
                                                             select new BO.TaskInEngineer()
                                                             {
                                                                 Id = doTask.Id,
                                                                 Alias = doTask.Alias
                                                             }).FirstOrDefault()
                                                 });
        return allEngineers;
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
            engineerTaskCanUpdated(boEngineer);
            DO.Task? task;
            if (boEngineer?.Task!.Id != 0)
            {
                task = _dal.Task.Read(boEngineer!.Task!.Id);
                if (task == null)
                    throw new BO.BlDoesNotExistException($"task with ID={boEngineer.Task.Id} does Not exist");
                _dal!.Task.Update(task! with { EngineerId = boEngineer.Id });

            }
            _dal!.Engineer.Update(doEngineer);
        }
        catch (DO.DalDoesNotExistException ex)
        {
            throw new BO.BlDoesNotExistException($"Engineer with ID={boEngineer.Id} does Not exist", ex);
        }
    }
    /// <summary>
    /// The function checks if it is possible to update the engineer task
    /// </summary>
    /// <param name="boEngineer">bo engineer for checking</param>
    /// <exception cref="BO.BlInvalidValuesException">Unable to update engineer's task</exception>
    private void engineerTaskCanUpdated(BO.Engineer boEngineer)
    {
        var unfinishedTask = (from DO.Task doTask in _dal.Task.ReadAll()
                     where doTask.EngineerId == boEngineer.Id &&
                      setStatus(doTask.Id)!= 4 //Checks if the task has not been completed
                              select doTask).FirstOrDefault();
        if (unfinishedTask != null && boEngineer!.Task!.Id != unfinishedTask.Id)
            throw new BO.BlInvalidValuesException($"It is not possible to update a task with ID={boEngineer.Task.Id} before its completion");
        DO.Task? taskEngineer = _dal.Task.Read(boEngineer!.Task!.Id);
        if (taskEngineer!=null&& taskEngineer.Id!= boEngineer.Task.Id)
            throw new BO.BlInvalidValuesException($"There is already an engineer for task with ID={boEngineer.Task.Id}");
        
    }
    /// <summary>
    /// Returns the status of an engineer's task
    /// </summary>
    /// <param name="id">ID of the desired task</param>
    /// <returns>The state of the task after calculation</returns>
    /// <exception cref="BO.BlDoesNotExistException">The requested task does not exist in the system</exception>
    private int setStatus(int id)
    {
        try
        {
            DO.Task? doTask = _dal.Task.Read(id);
            int status = 0;
            if (doTask!.Complete is not null)
                status = 4;
            else if (doTask.ForecastDate < DateTime.Now)
                status = 3;
            else if (doTask.Start < DateTime.Now)
                status = 2;
            else if (doTask.ForecastDate is not null)
                status = 1;
            else status = 0;
            return status;
        }
        catch (DO.DalDoesNotExistException ex)
        {
            throw new BO.BlDoesNotExistException($"Task with Id={id} was not found", ex);
        }
    }
}


