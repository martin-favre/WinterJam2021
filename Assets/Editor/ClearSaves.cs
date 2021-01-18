using UnityEditor;

public class ClearSaves 
{
    [MenuItem("Tools/Clear Saves")]
    public static void OnButtonPressed()
    {
        SaveManager.ClearSave();
    }
}

