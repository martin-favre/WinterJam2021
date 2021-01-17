using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveManager
{
    readonly static string savePath = Application.persistentDataPath + "/";
    BinaryFormatter bf = new BinaryFormatter();

    static SaveManager instance;

    public static SaveManager Instance { get => instance; }

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
        FileStream file = File.Open(GetPath(key), FileMode.OpenOrCreate);
        bf.Serialize(file, value);
        file.Close();

    }

    public object Load(string key)
    {
        string path = GetPath(key);
        if (!File.Exists(path)) return null;
        FileStream file = File.Open(GetPath(key), FileMode.Open);
        object retval = null;
        try
        {
            retval = bf.Deserialize(file) as object;
        }
        catch(Exception e)
        {
            file.Close();
            Debug.LogWarning(e.Message);
        }
        return retval;
    }

    public static void ClearSave() {
        var hi = Directory.GetFiles(savePath);
        foreach(var file in hi) {
            File.Delete(file);
        }
    }
}