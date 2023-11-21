

using DalApi;
using DO;
using System.Xml.Linq;

namespace Dal;

internal class EngineerImplementation : IEngineer
{
    const string s_engineer = "engineer";
    public int Create(Engineer item)
    {
        XElement rootEng = XMLTools.LoadListFromXMLElement(s_engineer);
        XElement newEng = new(s_engineer);
        XElement? find = rootEng.Elements("id")?.Where(p => p.Element("id")?.Value == item.Id.ToString()).FirstOrDefault();
        if (find != null)
        {
            throw new Exception($"Engineer with ID={item.Id} already exists");
        }
        else
        {
            rootEng.Add(newEng);

            XMLTools.SaveListToXMLElement(rootEng, s_engineer);
            return item.Id;
        }

    }

    public void Delete(int id)
    {
        throw new NotImplementedException();
    }

    public Engineer? Read(int id)
    {
        throw new NotImplementedException();
    }

    public Engineer? Read(Func<Engineer, bool> filter)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Engineer?> ReadAll(Func<Engineer?, bool>? filter = null)
    {
        throw new NotImplementedException();
    }

    public void Update(Engineer item)
    {
        throw new NotImplementedException();
    }
}
