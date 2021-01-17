using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractComponent : MonoBehaviour
{
    InteractType interactType = InteractType.None;

    SimpleObserver<InteractType> interactButtonObserver;

    static PlayerInteractComponent instance;

    public static PlayerInteractComponent Instance { get => instance; set => instance = value; }

    Item heldItem;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        interactButtonObserver = new SimpleObserver<InteractType>(InteractButtonHandler.Instance, OnInteractButtonPressed);
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (interactType != InteractType.None)
            {
                HandleInteractClick();
            }
            else if (heldItem != null)
            {
                HandleItemClick();
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            interactType = InteractType.None;
            heldItem = null;
        }
    }


    public void OnItemButtonPressed(Item item)
    {
        if (heldItem == null || heldItem.Type != item.Type)
        {
            GameLog.Instance.Log("Using the " + item.Type + ".");
            heldItem = item;
            interactType = InteractType.None;
        }
    }

    void OnInteractButtonPressed(InteractType type)
    {
        interactType = type;
        heldItem = null;
    }

    private void HandleInteractClick()
    {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

        if (hit.collider != null)
        {
            Debug.Log("Target Position: " + hit.collider.gameObject.transform.position);
            var comp = hit.collider.GetComponent<IInteractable>();
            comp?.Interact(interactType);
        }
    }
    private void HandleItemClick()
    {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

        if (hit.collider != null)
        {
            Debug.Log("Target Position: " + hit.collider.gameObject.transform.position);
            var comp = hit.collider.GetComponent<IInteractable>();
            comp?.Interact(heldItem);
            heldItem = null;
        }
    }

}
