using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasComponent : MonoBehaviour
{
    static GameObject instance; 
    void Awake() {
        if(instance == null) {
            instance = gameObject;
        } else if(instance.GetInstanceID() != gameObject.GetInstanceID()) {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }
    void Start()
    {
    }

    void Update()
    {
        
    }
}
