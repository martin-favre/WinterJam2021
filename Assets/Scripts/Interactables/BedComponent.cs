using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BedComponent : MonoBehaviour, IInteractable
{

    RotatingList lookAtMsgs = new RotatingList(new List<Msg>()
    {
        new Msg("This is your bed."),
        new Msg("It's a good bed.", true),
        new Msg("Not like any other bed."),
        new Msg("Your bed.", true)
    });

    RotatingList pickUpMsg = new RotatingList(new List<Msg>()
    {
        new Msg("You can't pick up your bed.", true),
        new Msg("Considering how often you go to the gym, not very likely."),
    });

    public void Interact(InteractType type)
    {
        switch (type)
        {
            case InteractType.LookAt:
                GameLog.Instance.Log(lookAtMsgs.GetNext());
                break;
            case InteractType.PickUp:
                GameLog.Instance.Log(pickUpMsg.GetNext());
            break;
        }
    }
}
