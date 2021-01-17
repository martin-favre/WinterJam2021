using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FridgeComponent : MonoBehaviour, IInteractable
{

    [System.Serializable]
    private class SaveData
    {
        public RotatingList lookAtMsgs;

        public RotatingList pickUpMsg;

    }

    SaveData data = new SaveData();

    const string saveKey = "FridgeComponent";

    void Awake()
    {
        object loadedData = SaveManager.Instance.Load(saveKey);
        if (loadedData == null)
        {
            data.lookAtMsgs = new RotatingList(new List<Msg>()
            {
                new Msg("Your fridge.", true),
                new Msg("There's probably still some leftovers inside.", true),
                new Msg("You don't feel hungry just yet."),
            });

            data.pickUpMsg = new RotatingList(new List<Msg>()
            {
                new Msg("You can't pick up your fridge.", true),
                new Msg("This would be a direct ticket to snap city."),
                new Msg("Your pickup game is not particularly strong."),
            });
        } else {
            data = (SaveData) loadedData;
        }
    }

    public void Interact(InteractType type)
    {
        switch (type)
        {
            case InteractType.LookAt:
                if (!Flags.Instance.IsFlagSet(Flags.FlagNames.HasCheese))
                {
                    GameLog.Instance.Log(data.lookAtMsgs.GetNext());
                }
                else
                {
                    GameLog.Instance.Log("The fridge is empty");
                }
                break;
            case InteractType.PickUp:
                GameLog.Instance.Log(data.pickUpMsg.GetNext());
                break;
            case InteractType.Use:
                HandleUse();
                break;
        }
        SaveManager.Instance.Save(saveKey, data);
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

    public void Interact(Item item)
    {
        switch(item.Type) {
            case Item.ItemType.Cheese:
                GameLog.Instance.Log("No need to return the " + item.Type + " just yet.");
            break;
        }
    }
}
