using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GameLog : MonoBehaviour
{
    List<string> logEntries = new List<string>();

    public TMPro.TMP_Text textComp;
    public Vector3 offset;

    static GameLog instance;

    public static GameLog Instance { get => instance; }

    void Awake() {
        if(instance == null) instance = this;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0)) {
            //Log(Time.time.ToString());
        }
    }

    void UpdateShownText()
    {
        var enumerable = Helpers.TakeLast<string>(logEntries, 6);
        textComp.text = "";
        foreach(string txt in enumerable) {
            textComp.text += txt + '\n';
        }
        
    }

    public void Log(string entry)
    {
        logEntries.Add(entry);
        UpdateShownText();
    }
}
