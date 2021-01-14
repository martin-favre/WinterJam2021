using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{

    Vector3 requestedMovement;
    public Vector2 movementSpeed;

    static PlayerMovementController instance;

    public static PlayerMovementController Instance { get => instance; }

    void Awake()
    {
        if(instance == null) instance = this;
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
        requestedMovement.x += Input.GetAxis("Horizontal") * movementSpeed.x;
        requestedMovement.y += Input.GetAxis("Vertical") * movementSpeed.y;
    }

    void FixedUpdate()
    {
        transform.position += requestedMovement * Time.deltaTime;
        requestedMovement = Vector3.zero;
    }
}
