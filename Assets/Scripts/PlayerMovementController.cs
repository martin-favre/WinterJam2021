using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour, IObservable<Vector2>
{

    Vector3 requestedMovement;
    public Vector2 movementSpeed;

    static PlayerMovementController instance;

    public static PlayerMovementController Instance { get => instance; }

    public Vector2 MaxPossibleMovement { get => movementSpeed; }

    List<IObserver<Vector2>> observers = new List<IObserver<Vector2>>();

    void Awake()
    {
        if (instance == null) instance = this;
    }
    void Start()
    {

    }

    void Update()
    {
        ReadMovementInput();
    }

    void ReadMovementInput()
    {
        requestedMovement.x = Input.GetAxis("Horizontal") * movementSpeed.x;
        requestedMovement.y = Input.GetAxis("Vertical") * movementSpeed.y;
    }

    void FixedUpdate()
    {
        transform.position += requestedMovement * Time.deltaTime;
        UpdateObservers(requestedMovement);
        requestedMovement = Vector3.zero;
    }

    public void UpdateObservers(Vector2 val)
    {
        foreach (var observer in observers)
        {
            observer.OnNext(val);
        }
    }

    public IDisposable Subscribe(IObserver<Vector2> observer)
    {
        return new GenericUnsubscriber<Vector2>(observers, observer);
    }
}
