using UnityEditor;

[CustomEditor(typeof(ClearSaves))]
public class ClearSaves 
{
    [MenuItem("Tools/Clear Saves")]
    public static void OnButtonPressed()
    {
        SaveManager.ClearSave();
    }
}

