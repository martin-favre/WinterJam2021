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
    SimpleObserver<List<Item>> observer;

    void Awake()
    {
        itemButtons.Add(this);
        buttonWidth = GetComponent<RectTransform>().rect.width;
        ReSortButtons();

    }

    void Start()
    {
        observer = new SimpleObserver<List<Item>>(InventoryComponent.Instance, (items) =>
        {
            if (indexInUi < items.Count)
            {
                if (!gameObject.activeInHierarchy) return;
                item = items[indexInUi];
                imageComp = Helpers.GetCompoonentInChildrenExceptParent<Image>(gameObject);
                imageComp.sprite = ItemImageLoader.Instance.GetItemSprite(item.Type);
            }
            else
            {
                item = null;
                imageComp.sprite = null;
            }
        });
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

    public void ClearItem()
    {
        this.item = null;
        imageComp.sprite = null;
    }

    public void OnClick()
    {
        if (this.item == null) return;
        PlayerInteractComponent.Instance.OnItemButtonPressed(item);
    }
}