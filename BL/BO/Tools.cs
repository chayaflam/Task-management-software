
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
    public static string ToStringProperty<T>(T item)
    {
        string result=""; 
        foreach (PropertyInfo prop in item.GetType().GetProperties())
        {
            result += prop.Name;
            result += " ";
            result += item.GetType().GetProperty(prop.Name)?.GetValue(item);
            result += "\n";
        }
        return result;

    }
}
