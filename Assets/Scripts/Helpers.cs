

using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class Helpers
{
    public static IEnumerable<T> TakeLast<T>(this IEnumerable<T> source, int N)
    {
        return source.Skip(Mathf.Max(0, source.Count() - N));
    }

}