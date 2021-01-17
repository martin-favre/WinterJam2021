using System.Collections.Generic;
using UnityEngine;

public class Flags
{
    public enum FlagNames
    {
        HasCheese,
        HasIOU,
    }
    HashSet<FlagNames> flags = new HashSet<FlagNames>();
    readonly static Flags instance;

    public static Flags Instance { get => instance;}

    static Flags()
    {
        instance = new Flags();
    }
    public void SetFlag(FlagNames name)
    {
        if (flags.Contains(name)) Debug.LogWarning("Setting flag " + name + " but it it's already set");
        flags.Add(name);
    }

    public void ClearFlag(FlagNames name)
    {
        bool success = flags.Remove(name);
        if (!success) Debug.LogWarning("Trying to clear flag " + name + " but it didn't exist");
    }

    public bool IsFlagSet(FlagNames name)
    {
        return flags.Contains(name);
    }
}