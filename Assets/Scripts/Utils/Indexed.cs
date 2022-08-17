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
}
