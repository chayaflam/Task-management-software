
using System.Reflection;

namespace BO;

public static class Tools<T> 
{
   /* Type type = typeof(T);
    static string? ToStringProperty()
    {
        return this.ToString();
    }
*/
    public static string ToStringProperty()
    {
        string s = "";
        Type type = typeof(T);
        foreach (PropertyInfo prop in type.GetProperties())
        {
            s += $"{prop.Name} : {type.GetProperty(prop.Name)?.GetValue(prop)}";
        }
        return s;
    }
}
