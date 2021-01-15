using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FridgeComponent : MonoBehaviour, IInteractable
{
    RotatingList lookAtMsgs;

    RotatingList pickUpMsg;

    void Awake()
    {
        lookAtMsgs = new RotatingList(new List<Msg>()
    {
        new Msg("Your fridge.", true),
        new Msg("There's probably still some leftovers inside.", true),
        new Msg("You don't feel hungry just yet."),
    });

        pickUpMsg = new RotatingList(new List<Msg>()
    {
        new Msg("You can't pick up your fridge.", true),
        new Msg("This would be a direct ticket to snap city."),
        new Msg("Your pickup game is not particularly strong."),
    });

    }

    public void Interact(InteractType type)
    {
        switch (type)
        {
            case InteractType.LookAt:
                if (!Flags.Instance.IsFlagSet(Flags.FlagNames.HasCheese))
                {
                    GameLog.Instance.Log(lookAtMsgs.GetNext());
                }
                else
                {
                    GameLog.Instance.Log("The fridge is empty");
                }
                break;
            case InteractType.PickUp:
                GameLog.Instance.Log(pickUpMsg.GetNext());
                break;
            case InteractType.Use:
                HandleUse();
                break;
        }
    }

    private void HandleUse()
    {
        if (!Flags.Instance.IsFlagSet(Flags.FlagNames.HasCheese))
        {
            InventoryComponent.Instance.AddItem(new Item(Item.ItemType.Cheese));
            GameLog.Instance.Log("You find a Cheese in the fridge.");
            GameLog.Instance.Log("The fridge is now empty.");
            Flags.Instance.SetFlag(Flags.FlagNames.HasCheese);
        }
    }
}
