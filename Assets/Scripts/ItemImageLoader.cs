using System;
using UnityEngine;

public class ItemImageLoader : MonoBehaviour
{
    // thanks https://answers.unity.com/questions/642431/dictionary-in-inspector.html
    [Serializable]
    public struct NamedImage
    {
        public Item.ItemType name;
        public Sprite image;
    }

    public NamedImage[] pictures;

    static ItemImageLoader instance;

    public static ItemImageLoader Instance { get => instance; }

    void Awake()
    {
        if (instance == null) instance = this;
    }

    public Sprite GetItemSprite(Item.ItemType type)
    {
        foreach (var img in pictures)
        {
            if (img.name == type) return img.image;
        }
        return null;
    }

}