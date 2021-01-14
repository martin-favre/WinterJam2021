using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractComponent : MonoBehaviour
{
    InteractType interactType = InteractType.None;

    SimpleObserver<InteractType> observer;
    void Start()
    {
        observer = new SimpleObserver<InteractType>(InteractButtonHandler.Instance, OnInteractButtonPressed);
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && interactType != InteractType.None)
        {
            HandleClick();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            interactType = InteractType.None;
        }
    }

    void OnInteractButtonPressed(InteractType type)
    {
        interactType = type;
    }

    private void HandleClick()
    {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

        if (hit.collider != null)
        {
            Debug.Log("Target Position: " + hit.collider.gameObject.transform.position);
            var comp = hit.collider.GetComponent<IInteractable>();
            comp?.Interact(interactType);
        }

    }
}
