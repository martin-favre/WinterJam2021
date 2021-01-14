using System;
using System.Collections.Generic;

public class RotatingList
{
    private List<Msg> insides;
    private int messageIndex = 0;

    public RotatingList(List<Msg> insides)
    {
        this.insides = insides;
    }

    public void RemoveText(Msg msg)
    {
        foreach (var myMsg in insides)
        {
            if (msg == myMsg)
            {
                insides.Remove(msg);
                if (messageIndex >= insides.Count)
                {
                    messageIndex = 0;
                }
                return;
            }
        }
    }
    public string GetNext()
    {
        Msg outMsg = insides[messageIndex];
        if (!outMsg.Repeat)
        {
            insides.RemoveAt(messageIndex);
        }
        else
        {
            messageIndex++;
        }
        if (messageIndex >= insides.Count)
        {
            messageIndex = 0;
        }
        return outMsg.Message;
    }
}