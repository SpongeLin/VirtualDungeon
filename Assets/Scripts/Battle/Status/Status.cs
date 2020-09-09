using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Status : Subscriber
{
    public string statusName;
    public int time = 1;
    public int num = 1;

    public bool byTime;
    public bool byNum;
    public bool eternal;
    public bool canRepeat;
    public void Repeat(Status s)
    {
        if (byTime)
            time += s.time;
        else if (byNum)
            num += s.num;
    }

    public bool display = false;

    public abstract void Enter();
    public virtual void Exit() { isWorking = false; }

    public string statusImage;
    public string showName;
    public abstract string GetDescription();
    public string GetNum()
    {
        if (byNum)
            return num.ToString();
        if (byTime)
            return time.ToString();
        return "";
    }


}


// Character Status
public abstract class CharStatus : Status
{
    public CharData character;


}