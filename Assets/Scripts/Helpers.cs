

using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class Helpers
{
    public static IEnumerable<T> TakeLast<T>(this IEnumerable<T> source, int N)
    {
        return source.Skip(Mathf.Max(0, source.Count() - N));
    }

    public static T GetCompoonentInChildrenExceptParent<T> (GameObject g) where T : Component{
        var comps = g.GetComponentsInChildren<T>();
        foreach(var comp in comps) {
            if(comp.gameObject.GetInstanceID() != g.GetInstanceID()){
                return comp;
            }
        }
        return null;
    }
}