namespace BlImplementation;
using BlApi;
using System;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Xml;

internal class EngineerImplementation : IEngineer
{
    private DalApi.IDal _dal =Factory.Get;

    public int Create(BO.Engineer boEngineer)
    {
        DO.Engineer doEngineer = new DO.Engineer
         (boEngineer.Id, boEngineer.Name, boEngineer.Email, boEngineer.Level, boEngineer.Cost);
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
    public void Delete(int id)
    {
       
    }

    public BO.Engineer? Read(int id)
    {
      
    }

    public IEnumerable<BO.Engineer?> ReadAll(Func<BO.Engineer?, bool>? filter = null)
    {
        
    }

    public void Update(BO.Engineer item)
    {
       
    }
}
