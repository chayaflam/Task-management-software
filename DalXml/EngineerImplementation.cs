

using DalApi;
using DO;
using System.Xml.Linq;

namespace Dal;

internal class EngineerImplementation : IEngineer
{
    const string s_engineer = "engineer";
    static DO.Engineer? getEngineer(XElement s) =>
       s.ToIntNullable("ID") is null ? null : new DO.Engineer()
       {
           Id = (int)s.Element("Id")!,
           Name = (string?)s.Element("Name"),
           Email = (string?)s.Element("Email"),
           Level = s.ToEnumNullable<DO.EngineerExperience>("Level"),
           Cost =s.ToDoubleNullable ("Cost")
       };
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
            if (item.Level is not null)
                newEng.Add(new XElement("Level", item.Level));
            XMLTools.SaveListToXMLElement(rootEng, s_engineer);
            return item.Id;
        }
    }

    public void Delete(int id)
    {
        XElement rootEng = XMLTools.LoadListFromXMLElement(s_engineer);
        XElement? find = rootEng.Elements("Id")?.Where(p => p.Element("Id")?.Value == id.ToString()).FirstOrDefault();
        if (find == null)
            throw new Exception($"Engineer with ID={id} does Not exist");
        else
        {
            find.Remove();
            XMLTools.SaveListToXMLElement(find, s_engineer);   
        }
    }

    public Engineer? Read(int id)
    {
        XElement rootEng = XMLTools.LoadListFromXMLElement(s_engineer);
        XElement find = rootEng.Elements(s_engineer)?.Where(p => p.Element("Id")?.Value == id.ToString()).FirstOrDefault()!;
        return  getEngineer(find);
    }

    public Engineer? Read(Func<Engineer, bool> filter)
    {
        XElement rootEng = XMLTools.LoadListFromXMLElement(s_engineer);
        XElement find = rootEng.Elements(s_engineer).Where(p=>filter(getEngineer(p)!)).FirstOrDefault()!;
        return getEngineer(find);
    }

    public IEnumerable<Engineer?> ReadAll(Func<Engineer?, bool>? filter = null)
    {
        XElement rootEng = XMLTools.LoadListFromXMLElement(s_engineer);
        if (filter == null)
            return rootEng.Elements().Select(p => getEngineer(p));
        else
            return rootEng.Elements().Select(p => getEngineer(p)).Where(filter);
    }

    public void Update(Engineer item)
    {
        XElement rootEng = XMLTools.LoadListFromXMLElement(s_engineer);
        XElement? find = rootEng.Elements(s_engineer).
            Where(eng =>eng.Element("Id")?.Value == item.Id.ToString()).FirstOrDefault();     
        
        if (find == null) throw new Exception($"Engineer with ID={item.Id} does Not exist");
        else
        {
            find.Remove();
            rootEng.Add(item);
            XMLTools.SaveListToXMLElement(rootEng, s_engineer);
        }
    }
}

