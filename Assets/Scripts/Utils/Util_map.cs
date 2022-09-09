using System;
using System.Collections.Generic;
public static class Util_Map
{
    public class UndefinableInversedMapException : Exception
    {
        public UndefinableInversedMapException(string message) : base(message){}
    }
    public static Dictionary<V, K> Inversed<K,V>(Dictionary<K,V> map)
    {
        Dictionary<V, K> inversed = new();
        foreach(K key in map.Keys)
        {
           if (!map.TryGetValue(key, out V value)) throw new KeyNotFoundException($"Given key {key} is not found.");
           if (!inversed.TryAdd(value, key)) throw new UndefinableInversedMapException($"Given map is not an injection because of key {key}.");

        }
        return inversed;
    }
}
