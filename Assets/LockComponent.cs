using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockComponent : MonoBehaviour, IInteractable
{
    [Serializable]
    private class SaveData
    {
        public RotatingList lookAtMsg;
        public RotatingList pickUpMsg;
        public RotatingList useMsg;
        public bool unlocked = false;
    }
    SaveData data = new SaveData();
    const string saveKey = "LockComponent";
    public GameObject door;
    void Awake()
    {
        object loadedData = SaveManager.Instance.Load(saveKey);
        if (loadedData == null)
        {
            data.pickUpMsg = new RotatingList(
                new List<Msg>() {
                    new Msg("You can't pick up the Lock", true),
                    new Msg("You try to rip the Lock from the door, unsuccessfully."),
                    new Msg("You try to seduce the Lock, unsuccessfully."),
                }
            );

            data.lookAtMsg = new RotatingList(
                  new List<Msg>() {
                  new Msg("The door is locked.", true),
                }
            );

            data.useMsg = new RotatingList(
                    new List<Msg>() {
                        new Msg("You can't use Lock like this.", true),
                    }
                );
        }
        else
        {
            data = (SaveData)loadedData;
        }
        if (data.unlocked)
        {
            door.GetComponent<Collider2D>().enabled = true;
            gameObject.SetActive(false);
        } else {
             door.GetComponent<Collider2D>().enabled = false;
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
                GameLog.Instance.Log(data.pickUpMsg.GetNext());
                break;
            case InteractType.Use:
                GameLog.Instance.Log(data.useMsg.GetNext());
                break;
        }
        SaveManager.Instance.Save(saveKey, data);
    }

    public void Interact(Item item)
    {
        switch (item.Type)
        {
            case Item.ItemType.Key:
                HandleKey();
                break;
        }
        SaveManager.Instance.Save(saveKey, data);
    }

    private void HandleKey()
    {
        data.unlocked = true;
        InventoryComponent.Instance.RemoveItem(Item.ItemType.Key);
        GameLog.Instance.Log("You unlock the door.");
        gameObject.SetActive(false);
        door.GetComponent<Collider2D>().enabled = true;
    }
}
