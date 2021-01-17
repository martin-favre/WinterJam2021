using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseHoleComponent : MonoBehaviour, IInteractable
{
    [Serializable]
    private class SaveData
    {
        public RotatingList lookAtMsg;
        public RotatingList pickUpMsg;
        public RotatingList useMsg;
    }
    SaveData data = new SaveData();
    const string saveKey = "MouseHoleComponent";
    void Awake()
    {
        object loadedData = SaveManager.Instance.Load(saveKey);
        if (loadedData == null)
        {
            data.pickUpMsg = new RotatingList(
                new List<Msg>() {
                    new Msg("You can't Pick Up a mouse hole", true),
                    new Msg("How do you expect this to work."),
                    new Msg("No a chance"),
                }
            );

            data.lookAtMsg = new RotatingList(
                  new List<Msg>() {
                  new Msg("A mouse hole in the wall.", true),
                  new Msg("There are sounds coming from inside."),
                  new Msg("It looks like someone lives there.", true)
                }
            );

            data.useMsg = new RotatingList(
                    new List<Msg>() {
                        new Msg("The mouse refuses your commands.", true),
                        new Msg("The owner is a union member and does not work for free."),
                        new Msg("Maybe the rodent could be convinced somehow."),
                    }
                );
        }
        else
        {
            data = (SaveData)loadedData;
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
            case Item.ItemType.Cheese:
                HandleCheese();
                break;
        }
        SaveManager.Instance.Save(saveKey, data);
    }

    private void HandleCheese()
    {
        if (!Flags.Instance.IsFlagSet(Flags.FlagNames.HasIOU))
        {
            GameLog.Instance.Log("The mouse happily accepts your cheese and gives you an IOU as thanks");
            InventoryComponent.Instance.RemoveItem(Item.ItemType.Cheese);
            InventoryComponent.Instance.AddItem(new Item(Item.ItemType.IOU));
            Flags.Instance.SetFlag(Flags.FlagNames.HasIOU);
        }
        else
        {
            Debug.LogWarning("How did the player get another cheese?");
        }
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
