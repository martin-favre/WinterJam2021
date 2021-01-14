using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollPanelComponent : MonoBehaviour
{
    int maxIndex = 0;

    static ScrollPanelComponent instance;

    public static ScrollPanelComponent Instance { get => instance;}

    void Awake(){
        if(instance == null) instance = this;
    }
    void Start()
    {

    }

    void Update()
    {

    }

    void UpdateWidth() {
        //float width = maxIndex * (ItemButtonComponent.buttonWidth + ItemButtonComponent.spaceBetweenButtons);
        //var r = GetComponent<RectTransform>();
        //GetComponent<RectTransform>().sizeDelta = new Vector2(width, r.sizeDelta.y);
        //GetComponent<RectTransform>().rect.Set(r.x, r.y, width, r.height);
    }

    public void RegisterItemButton(ItemButtonComponent comp)
    {
        if(comp.indexInUi > maxIndex) {
            maxIndex = comp.indexInUi;
            UpdateWidth();
        }
    }
}
