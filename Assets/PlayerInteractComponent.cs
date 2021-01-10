using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractComponent : MonoBehaviour
{
    List<InteractableComponent> nearbyInteractables = new List<InteractableComponent>();
    void Start()
    {

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            HandleInteraction();
        }
    }

    private void HandleInteraction()
    {
        if (nearbyInteractables.Count > 0)
        {
            if (nearbyInteractables.Count > 1) Debug.LogWarning("Interacting with multiple objects, only handling one of them");
            var comp = nearbyInteractables[0];
            comp.Interact();
        }


    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        var comp = collider.GetComponent<InteractableComponent>();
        if (comp) nearbyInteractables.Add(comp);
    }
    private void OnTriggerExit2D(Collider2D collider)
    {
        var comp = collider.GetComponent<InteractableComponent>();
        if (comp) nearbyInteractables.Remove(comp);
    }
}
