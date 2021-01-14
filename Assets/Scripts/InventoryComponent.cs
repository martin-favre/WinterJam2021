using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InventoryComponent : MonoBehaviour
{

    List<Item> items = new List<Item>();

    // thanks https://answers.unity.com/questions/642431/dictionary-in-inspector.html
    [Serializable]
    public struct NamedImage
    {
        public Item.ItemType name;
        public Sprite image;
    }
    public NamedImage[] pictures;

    static InventoryComponent instance;

    public static InventoryComponent Instance { get => instance; }

    void Awake()
    {
        if (instance == null) instance = this;
    }
    void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        
    }

    public void AddItem(Item item)
    {
        items.Add(item);
        GetLastFreeItemButton().SetItem(item);
    }

    

    public Sprite GetItemSprite(Item.ItemType type)
    {
        foreach (var img in pictures)
        {
            if (img.name == type) return img.image;
        }
        return null;
    }

    private ItemButtonComponent GetLastFreeItemButton()
    {
        foreach (var button in ItemButtonComponent.itemButtons)
        {
            if (button.Item == null)
            {
                return button;
            }
        }
        return null;
    }

    void RemoveItemFromButtons(Item item)
    {
        foreach (var button in ItemButtonComponent.itemButtons)
        {
            if (button.Item.Type == item.Type)
            {
                button.ClearItem();
                return;
            }
        }

    }

    public void RemoveItem(Item item)
    {
        items.Remove(item);
    }
}