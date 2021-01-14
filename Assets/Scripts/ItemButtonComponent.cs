using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemButtonComponent : MonoBehaviour
{

    public int indexInUi;
    public static List<ItemButtonComponent> itemButtons = new List<ItemButtonComponent>();
    public static float buttonWidth;
    public const float spaceBetweenButtons = 15;
    private Item item;

    public Item Item { get => item; }
    public Image imageComp;

    void Awake()
    {
        itemButtons.Add(this);
        buttonWidth = GetComponent<RectTransform>().rect.width;
        ReSortButtons();
        
    }

    void Start() {
        //transform.position = itemButtons[0].transform.position + new Vector3(buttonWidth + spaceBetweenButtons, 0, 0) * indexInUi;
        //ScrollPanelComponent.Instance.RegisterItemButton(this);
    }

    void ReSortButtons()
    {
        itemButtons.Sort((a, b) =>
        {
            if (a.indexInUi > b.indexInUi) return 1;
            if (a.indexInUi < b.indexInUi) return -1;
            return 0;
        });
    }

    void OnDestroy()
    {
        itemButtons.Remove(this);
        ReSortButtons();
    }
    public void SetItem(Item item)
    {
        this.item = item;
        imageComp.sprite = InventoryComponent.Instance.GetItemSprite(item.Type);
    }

    public void ClearItem() {
        this.item = null;
        imageComp.sprite = null;
    }
}