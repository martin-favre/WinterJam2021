using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractButtonHandler : MonoBehaviour, IObservable<InteractType>
{

    static InteractButtonHandler instance;
    List<IObserver<InteractType>> observers = new List<IObserver<InteractType>>();

    public static InteractButtonHandler Instance { get => instance; }

    void Awake()
    {
        if (instance == null) instance = this;
    }
    public IDisposable Subscribe(IObserver<InteractType> observer)
    {
        return new GenericUnsubscriber<InteractType>(observers, observer);
    }

    private void UpdateSubscribers(InteractType type)
    {
        foreach (var observer in observers)
        {
            observer.OnNext(type);
        }
    }

    public void OnLookAtButtonPressed()
    {
        UpdateSubscribers(InteractType.LookAt);
    }

    public void OnUseButtonPressed()
    {
        UpdateSubscribers(InteractType.Use);
    }

    public void OnPickUpButtonPressed()
    {
        UpdateSubscribers(InteractType.PickUp);

    }
}
