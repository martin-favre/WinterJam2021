using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.Networking;

public class SaveManager
{
    readonly static string savePath = Application.persistentDataPath + "/";
    BinaryFormatter bf = new BinaryFormatter();

    static SaveManager instance;

    public static SaveManager Instance { get => instance; }

    Dictionary<string, object> saves = new Dictionary<string, object>();

    static SaveManager()
    {
        instance = new SaveManager();
    }

    string GetPath(string key)
    {
        return savePath + key;
    }
    public void Save(string key, object value)
    {
        saves[key] = value;
    }

    public object Load(string key)
    {
        object obj;
        saves.TryGetValue(key, out obj);
        return obj;
    }

    public static void ClearSave()
    {
        var hi = Directory.GetFiles(savePath);
        foreach (var file in hi)
        {
            File.Delete(file);
        }
    }
}