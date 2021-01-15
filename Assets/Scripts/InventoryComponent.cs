using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InventoryComponent : IObservable<List<Item>>
{
    List<Item> items = new List<Item>();

    List<IObserver<List<Item>>> observers = new List<IObserver<List<Item>>>();

    static InventoryComponent instance;

    public static InventoryComponent Instance { get => instance; }

    static InventoryComponent()
    {
        instance = new InventoryComponent();
    }

    public void AddItem(Item item)
    {
        items.Add(item);
        UpdateObservers();
    }

    public void RemoveItem(Item item)
    {
        items.Remove(item);
        UpdateObservers();
    }

    private void UpdateObservers()
    {
        foreach (var obs in observers)
        {
            obs.OnNext(items);
        }
    }

    public IDisposable Subscribe(IObserver<List<Item>> observer)
    {
        observer.OnNext(items);
        return new GenericUnsubscriber<List<Item>>(observers, observer);
    }
}