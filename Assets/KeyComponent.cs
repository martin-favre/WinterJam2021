using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyComponent : MonoBehaviour, IInteractable
{
    [Serializable]
    private class SaveData
    {
        public RotatingList lookAtMsg;
        public RotatingList useMsg;
        public bool pickedUp = false;
    }
    SaveData data = new SaveData();
    const string saveKey = "KeyComponent";
    void Awake()
    {
        object loadedData = SaveManager.Instance.Load(saveKey);
        if (loadedData == null)
        {
            data.lookAtMsg = new RotatingList(
                  new List<Msg>() {
                  new Msg("The outdoor key.", true),
                  new Msg("Why did you put this here?"),
                }
            );

            data.useMsg = new RotatingList(
                    new List<Msg>() {
                        new Msg("You can't use the key.", true),
                        new Msg("Whilst you technically CAN use keys, this context doesn't make sense."),
                    }
                );
        }
        else
        {
            data = (SaveData)loadedData;
        }
        if(data.pickedUp) {
            gameObject.SetActive(false);
        }
    }

    public void Interact(InteractType type)
    {
        switch (type)
        {
            case InteractType.LookAt:
                GameLog.Instance.Log(data.lookAtMsg.GetNext());
                break;
            case InteractType.PickUp:
                HandlePickup();
                break;
            case InteractType.Use:
                GameLog.Instance.Log(data.useMsg.GetNext());
                break;
        }
        SaveManager.Instance.Save(saveKey, data);
    }

    private void HandlePickup()
    {
        data.pickedUp = true;
        GameLog.Instance.Log("You picked up the key.");
        gameObject.SetActive(false);
        InventoryComponent.Instance.AddItem(new Item(Item.ItemType.Key));
        Flags.Instance.SetFlag(Flags.FlagNames.HasKey);
    }

    public void Interact(Item item)
    {
        GameLog.Instance.Log("You can't use these items together");
        SaveManager.Instance.Save(saveKey, data);
    }

}
