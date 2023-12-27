namespace BlImplementation;
using BlApi;
using System;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Xml;
using System.Data.Common;

internal class EngineerImplementation : IEngineer
{
    private DalApi.IDal _dal = Factory.Get;

    public int Create(BO.Engineer boEngineer)
    {
        if (boEngineer.Id <= 0 || boEngineer.Name == "" || boEngineer.Name == null ||
           boEngineer.Email == "" || boEngineer.Email == null || boEngineer.Cost <= 0)
            throw new bo();


        DO.Engineer doEngineer = new DO.Engineer
         (boEngineer.Id, boEngineer.Name, boEngineer.Email, (DO.EngineerExperience?)boEngineer.Level, boEngineer.Cost);
        try
        {
            int idEngineer = _dal.Engineer.Create(doEngineer);
            return idEngineer;
        }
        catch (DO.DalAlreadyExistsException ex)
        {
            // throw new BO.BlAlreadyExistsException($"Engineer with ID={boEngineer.Id} already exists", ex);
            return 0;
        }
    }

    public void Delete(int id)
    {
        DO.Engineer? doEngineer = _dal.Engineer.Read(id);
        if (doEngineer == null)
            throw new BO.BlDoesNotExistException($"Engineer with ID={id} does Not exist");

        List<DO.Task?> tasks = (List<DO.Task?>)_dal.Task.ReadAll();
        DO.Task? task = tasks.FirstOrDefault(x => x!.EngineerId == id);
        if (task != null)
        {
            throw new BO.BlNotErasable($"Engineer with ID={id}cannot be deleted");
        }
        try
        {
            _dal.Engineer.Delete(id);
        }
        catch (Exception ex)
        {
            throw new BO.BlNotErasable($"Engineer with ID={id}cannot be deleted");
        }

    }

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

    public IEnumerable<BO.Engineer?> ReadAll(Func<BO.Engineer?, bool>? filter = null)
    {
        return (from DO.Engineer doEngineer in _dal.Engineer.ReadAll()
                select new BO.Engineer()
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

    public void Update(BO.Engineer boEngineer)
    {
        if (boEngineer.Id <= 0 || boEngineer.Name == "" ||
           boEngineer.Email == "" || boEngineer.Cost <= 0)
            throw new bo();
            DO.Engineer doEngineer = new DO.Engineer
             (boEngineer.Id, boEngineer.Name, boEngineer.Email, (DO.EngineerExperience?)boEngineer.Level, boEngineer.Cost);
        try
            {
                _dal.Engineer.Update(doEngineer);
            }
            catch (Exception ex)
            {
            throw new BO.bl();
            }



    }
}
