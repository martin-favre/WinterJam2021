
using System;
[Serializable]
public class Msg
{
    string message;
    bool repeat;
    public Msg(string msg)
    {
        this.message = msg;
        this.repeat = false;
    }
    public Msg(string msg, bool repeat)
    {
        this.message = msg;
        this.repeat = repeat;
    }

    public string Message { get => message; set => message = value; }
    public bool Repeat { get => repeat; set => repeat = value; }
}