using System.Collections.Generic;
using System;

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

    public static bool x_in_collection<T>(T x, IEnumerable<T> collection)
    {
        foreach (T element in collection) if (element.Equals(x)) return true;
        return false;
    }
    public static void deleteFromList<T>(IEnumerable<T> deletedElements, List<T> list)
    {
        foreach (T element in deletedElements) list.Remove(element);
    }
}
