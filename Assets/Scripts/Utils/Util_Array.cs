using System.Collections.Generic;

public partial class Util_Array
{
    public static IEnumerable<(int, T)> Indexed<T>(T[] target)
    {
        int index = 0;
        foreach (T element in target)
        {
            yield return (index, element);
            index++;
            
        }
    }
    public static List<T> convertToList<T>(IEnumerable<T> collection)
    {
        var list = new List<T>();
        foreach (T key in collection) list.Add(key);
        return list;
    }
}
