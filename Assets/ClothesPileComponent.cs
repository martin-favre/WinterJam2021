using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClothesPileComponent : MonoBehaviour, IInteractable
{
    [Serializable]
    private class SaveData
    {
        public RotatingList lookAtMsg;
        public RotatingList pickUpMsg;
        public RotatingList useMsg;
        public bool eaten = false;
    }
    SaveData data = new SaveData();
    const string saveKey = "ClothesPileComponent";
    void Awake()
    {
        object loadedData = SaveManager.Instance.Load(saveKey);
        if (loadedData == null)
        {
            data.pickUpMsg = new RotatingList(
                new List<Msg>() {
                    new Msg("The laundry pile is too large to pick up", true),
                    new Msg("."),
                    new Msg("No a chance"),
                }
            );

            data.lookAtMsg = new RotatingList(
                  new List<Msg>() {
                  new Msg("A pile of dirty laundry blocking the Bathroom door.", true),
                  new Msg("You wonder why you decided to put these here."),
                  new Msg("The smell permeates the room."),
                  new Msg("Did those undies just move?")
                }
            );

            data.useMsg = new RotatingList(
                    new List<Msg>() {
                        new Msg("You can't use the laundry pile.", true),
                        new Msg("Even if you could use it, you don't know if you'd want to."),
                    }
                );
        }
        else
        {
            data = (SaveData)loadedData;
        }
        if(data.eaten) {
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
            case Item.ItemType.IOU:
                HandleIOU();
                break;
        }
        SaveManager.Instance.Save(saveKey, data);
    }

    private void HandleIOU()
    {
        data.eaten = true;
        InventoryComponent.Instance.RemoveItem(Item.ItemType.IOU);
        GameLog.Instance.Log("The mouse and its family eats the laundry pile");
        gameObject.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
