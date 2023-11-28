

using DalApi;
using DO;
using System.Xml.Linq;

namespace Dal;

internal class EngineerImplementation : IEngineer
{
    const string s_engineer = "engineers";
    /// <summary>
    /// Converts an xml engineer to engineer object
    /// </summary>
    static Engineer? getEngineer(XElement s) =>

       s.ToIntNullable("Id") is null ? null : new DO.Engineer()
       {
           Id = (int)s.Element("Id")!,
           Name = (string?)s.Element("Name"),
           Email = (string?)s.Element("Email"),
           Level =s.ToEnumNullable<DO.EngineerExperience>("Level")?? EngineerExperience.Novice,
           Cost =s.ToDoubleNullable ("Cost")
       };
    /// <summary>
    /// create engineer
    /// </summary>
    public int Create(Engineer item)
    {
        XElement rootEng = XMLTools.LoadListFromXMLElement(s_engineer);
        XElement newEng = new(s_engineer);
        XElement? find = rootEng.Elements(s_engineer)?.Where(p => p.Element("Id")?.Value == item.Id.ToString()).FirstOrDefault();
        if (find != null)
        {
            throw new Exception($"Engineer with ID={item.Id} already exists");
        }
        else
        {
            rootEng.Add(newEng);
            newEng.Add(new XElement("Id", item.Id));
            if (item.Name is not null)
                newEng.Add(new XElement("Name", item.Name));
            if (item.Email is not null)
                newEng.Add(new XElement("Email", item.Email));
            if (item.Cost is not null)
                newEng.Add(new XElement("Cost", item.Cost));
            //if (item.Level is not null)
                newEng.Add(new XElement("Level", item.Level));
            XMLTools.SaveListToXMLElement(rootEng, s_engineer);
            return item.Id;
        }
    }
    /// <summary>
    /// delete engineer
    /// </summary>
    public void Delete(int id)
    {
        XElement rootEng = XMLTools.LoadListFromXMLElement(s_engineer);
        XElement? find = rootEng.Elements()?.Where(p => p.Element("Id")?.Value == id.ToString()).FirstOrDefault();
        if (find == null)
            throw new Exception($"Engineer with ID={id} does Not exist");
        else
        {
            find.Remove();
            XMLTools.SaveListToXMLElement(rootEng, s_engineer);   
        }
    }
    /// <summary>
    /// read engineer by id
    /// </summary>
    public Engineer? Read(int id)
    {
        XElement rootEng = XMLTools.LoadListFromXMLElement(s_engineer);
        XElement find = rootEng.Elements(s_engineer)?.Where(p => p.Element("Id")?.Value == id.ToString()).FirstOrDefault()!;
        if (find != null)
            return getEngineer(find);
        else
            return null;
    }
    /// <summary>
    /// read a engineer according to a certain condition
    /// </summary>
    public Engineer? Read(Func<Engineer, bool> filter)
    {
        XElement rootEng = XMLTools.LoadListFromXMLElement(s_engineer);
        XElement find = rootEng.Elements(s_engineer).Where(p=>filter(getEngineer(p)!)).FirstOrDefault()!;
        return getEngineer(find);
    }
    /// <summary>
    /// read all engineers
    /// </summary>
    public IEnumerable<Engineer> ReadAll(Func<Engineer, bool>? filter = null)
    {
        XElement rootEng = XMLTools.LoadListFromXMLElement(s_engineer);
        if (filter == null) {
            IEnumerable<Engineer?> engineers = rootEng.Elements().Select(e=> getEngineer(e));
            return engineers!;
        }
        else
            return rootEng.Elements().Select(p => (getEngineer(p))).Where(filter)!;
    }
    /// <summary>
    /// update engineer
    /// </summary>
    public void Update(Engineer item)
    {
        XElement rootEng = XMLTools.LoadListFromXMLElement(s_engineer);
        XElement? find = rootEng.Elements(s_engineer).
            Where(eng =>eng.Element("Id")?.Value == item.Id.ToString()).FirstOrDefault();     
        
        if (find == null) throw new Exception($"Engineer with ID={item.Id} does Not exist");
        else
        {
            XElement newEng = new(s_engineer);
            rootEng.Add(newEng);
            newEng.Add(new XElement("Id", item.Id));
            if (item.Name is not null)
                newEng.Add(new XElement("Name", item.Name));
            else
                newEng.Add(new XElement("Name", find.Element("Name")?.Value));
            if (item.Email is not null)
                newEng.Add(new XElement("Email", item.Email));
            else
                newEng.Add(new XElement("Email", find.Element("Email")?.Value));
            if (item.Cost is not null)
                newEng.Add(new XElement("Cost", item.Cost));
            else
                newEng.Add(new XElement("Cost", find.Element("Cost")?.Value));
            if (item.Level is not null)
                newEng.Add(new XElement("Level", item.Level));
            else
                newEng.Add(new XElement("Level", find.Element("Level")?.Value));
            find.Remove();
           
            XMLTools.SaveListToXMLElement(rootEng, s_engineer);
        }
    }
}

